using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Features.LibraryMembers.Commands;
using LibraryManagement.Application.Features.LibraryMembers.Query;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;
using LibraryManagementC.Domain.Enums;
using MediatR;

namespace LibraryManagement.Application.Features.LibraryMembers.Handlers
{
    public class BorrowBookCommadHandler : ICommandHandler<BorrowBookCommand>
    {
        private readonly IGenericWriteRepository<Member> _memberRepo;
        private readonly IGenericWriteRepository<Book> _bookWriteRepository;
        private readonly IGenericWriteRepository<BorrowHistory> _bookHistoryWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BorrowBookCommadHandler(
            IGenericWriteRepository<Member> memberRepo,
            IGenericWriteRepository<Book> bookWriteRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMapper mapper,
            IGenericWriteRepository<BorrowHistory> bookHistoryWriteRepository)
        {
            _memberRepo = memberRepo;
            _bookWriteRepository = bookWriteRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
            _bookHistoryWriteRepository = bookHistoryWriteRepository;
        }

        public async Task<Result> Handle(
            BorrowBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var book = await _bookWriteRepository.Get(command.BookID);

            var member = await _memberRepo.Get(command.MemberId);

            if (book == null)
                return Result.Failure(
                    new Error(
                        $"404",
                        $"Book with id {command.BookID} not found"
                    )
                );

            if (member == null)
                return Result.Failure(
                    new Error(
                        $"404",
                        $"Member with id {command.MemberId} not found"
                    )
                );

            if (book.Status == BookStatus.BORROWED)
                return Result.Failure(
                    new Error(
                        $"404",
                        $"Book with id {command.BookID} is already borrowed"
                    )
                );

            book.Status = BookStatus.BORROWED;
            book.Member = member;
            book.MemberId = member.Id;

            var borrowHistory = new BorrowHistory()
            {
                BookId = book.Id,
                MemberId = member.Id,
                Book = book,
                Member = member,
                BorrowedDate = DateOnly.FromDateTime(DateTime.Now),
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
            };

            member.AddBook(book);

            await _bookHistoryWriteRepository.AddEntityAsync(borrowHistory);
            _bookWriteRepository.UpdateEntityAsync(book);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Borrowed Succesfully");
        }
    }
}
