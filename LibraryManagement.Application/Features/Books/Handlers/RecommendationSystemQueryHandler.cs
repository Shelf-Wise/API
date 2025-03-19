using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class RecommendationSystemQueryHandler : IQueryHandler<RecommendationSystemQuery, Result<List<GetAllBooksResultDto>>>
    {
        private readonly IGenericWriteRepository<BorrowHistory> _borrowHistoryRepository;
        private readonly IGenericWriteRepository<Book> _bookRepository;
        private readonly IGenericWriteRepository<Member> _memberRepository;
        private readonly IMapper _mapper;

        public RecommendationSystemQueryHandler(
            IGenericWriteRepository<BorrowHistory> borrowHistoryRepository,
            IGenericWriteRepository<Book> bookRepository,
            IGenericWriteRepository<Member> memberRepository,
            IMapper mapper)
        {
            _borrowHistoryRepository = borrowHistoryRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBooksResultDto>>> Handle(RecommendationSystemQuery query, CancellationToken cancellationToken)
        {
            var memberId = query.MemberId;
            int numberOfRecommendations = 5;

            // Get hybrid recommendations
            var recommendedBooks = await GetHybridRecommendations(memberId, numberOfRecommendations);

            // Map to DTO
            return Result.Success(_mapper.Map<List<GetAllBooksResultDto>>(recommendedBooks));
        }

        private async Task<List<Book>> GetContentBasedRecommendations(Guid memberId, int numberOfRecommendations = 5)
        {
            // Get all borrow history for this member
            var borrowHistories = await _borrowHistoryRepository.GetAllAsync(
                predicate: bh => bh.MemberId == memberId,
                includeProperties: "Book.Genres");

            // Take the most recent 10 borrows
            var recentBorrows = borrowHistories
                .OrderByDescending(bh => bh.BorrowedDate)
                .Take(10)
                .ToList();

            // Count genre frequency
            Dictionary<Guid, int> genreFrequency = new Dictionary<Guid, int>();
            foreach (var borrow in recentBorrows)
            {
                foreach (var genre in borrow.Book.Genres)
                {
                    if (genreFrequency.ContainsKey(genre.Id))
                        genreFrequency[genre.Id]++;
                    else
                        genreFrequency[genre.Id] = 1;
                }
            }

            // Get preferred genres
            var preferredGenreIds = genreFrequency
                .OrderByDescending(kv => kv.Value)
                .Take(3)
                .Select(kv => kv.Key)
                .ToList();

            // Get books with these genres that the member hasn't borrowed yet
            var borrowedBookIds = borrowHistories.Select(bh => bh.BookId).ToList();

            var allBooks = await _bookRepository.GetAllWithInclude(
                b => b.Genres);

            var recommendedBooks = allBooks
                .Where(b => b.Genres.Any(g => preferredGenreIds.Contains(g.Id)) &&
                           !borrowedBookIds.Contains(b.Id))
                .OrderBy(b => Guid.NewGuid()) // Random order as we don't have a rating system
                .Take(numberOfRecommendations)
                .ToList();

            return recommendedBooks;
        }

        private async Task<List<Book>> GetCollaborativeFilteringRecommendations(Guid memberId, int numberOfRecommendations = 5)
        {
            // Get all borrow histories
            var allBorrowHistories = await _borrowHistoryRepository.GetAllAsync();

            // Get the target member's borrowing history
            var memberBorrowedBookIds = allBorrowHistories
                .Where(bh => bh.MemberId == memberId)
                .Select(bh => bh.BookId)
                .ToList();

            // Group borrow histories by member
            var memberBorrowings = allBorrowHistories
                .Where(bh => bh.MemberId != memberId)
                .GroupBy(bh => bh.MemberId)
                .Select(g => new
                {
                    MemberId = g.Key,
                    BorrowedBooks = g.Select(bh => bh.BookId).ToList()
                })
                .ToList();

            // Find similar members
            var similarMembers = memberBorrowings
                .Select(m => new
                {
                    MemberId = m.MemberId,
                    CommonBooks = m.BorrowedBooks.Intersect(memberBorrowedBookIds).Count(),
                    TotalBooks = m.BorrowedBooks.Count
                })
                .Where(m => m.CommonBooks > 0)
                .OrderByDescending(m => (double)m.CommonBooks / m.TotalBooks)
                .Take(5)
                .Select(m => m.MemberId)
                .ToList();

            // Get books borrowed by similar members but not by the target member
            var similarMemberBorrows = allBorrowHistories
                .Where(bh => similarMembers.Contains(bh.MemberId) && !memberBorrowedBookIds.Contains(bh.BookId))
                .ToList();

            // Count frequency of each book
            var bookFrequency = similarMemberBorrows
                .GroupBy(bh => bh.BookId)
                .Select(g => new { BookId = g.Key, Count = g.Count() })
                .OrderByDescending(b => b.Count)
                .Take(numberOfRecommendations)
                .ToList();

            // Get the actual books
            var recommendedBooks = new List<Book>();
            foreach (var bookFreq in bookFrequency)
            {
                var book = await _bookRepository.Get(bookFreq.BookId);
                if (book != null)
                    recommendedBooks.Add(book);
            }

            return recommendedBooks;
        }

        private async Task<List<Book>> GetHybridRecommendations(Guid memberId, int numberOfRecommendations = 5)
        {
            var contentBasedRecs = await GetContentBasedRecommendations(memberId, numberOfRecommendations);
            var collaborativeRecs = await GetCollaborativeFilteringRecommendations(memberId, numberOfRecommendations);

            // Combine and weight results
            Dictionary<Guid, double> bookScores = new Dictionary<Guid, double>();

            foreach (var book in contentBasedRecs)
            {
                bookScores[book.Id] = 0.7; // Weight for content-based recommendations
            }

            foreach (var book in collaborativeRecs)
            {
                if (bookScores.ContainsKey(book.Id))
                    bookScores[book.Id] += 0.3; // Add collaborative filtering weight
                else
                    bookScores[book.Id] = 0.3; // Weight for collaborative recommendations
            }

            // Get top recommendations
            var topBookIds = bookScores
                .OrderByDescending(kv => kv.Value)
                .Take(numberOfRecommendations)
                .Select(kv => kv.Key)
                .ToList();

            // Get the actual books
            var recommendedBooks = new List<Book>();
            foreach (var bookId in topBookIds)
            {
                var book = await _bookRepository.GetByIdAsync(bookId);
                if (book != null)
                    recommendedBooks.Add(book);
            }

            return recommendedBooks;
        }
    }
}