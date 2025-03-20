using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class GetBorrowedBooksByMemberIdQueryHandler : IQueryHandler<GetBorrowedBooksByMemberIdQuery, Result<List<GetBorrowedBooksByMemberIdResponse>>>
    {
        private readonly IGenericWriteRepository<BorrowHistory> repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public GetBorrowedBooksByMemberIdQueryHandler(IGenericWriteRepository<BorrowHistory> repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetBorrowedBooksByMemberIdResponse>>> Handle(GetBorrowedBooksByMemberIdQuery query, CancellationToken cancellationToken)
        {
            var borrowedBooks = await repository.GetAllWithInclude(
                   b => b.Member,
                   b => b.Book);

            if (borrowedBooks == null)
                return Result.Failure(
                    new List<GetBorrowedBooksByMemberIdResponse>(),
                    new Error("400", "No Data Found")
                );

            var filteredBooks = borrowedBooks.Where(b => b.MemberId == query.MemberId).ToList();

            if (!filteredBooks.Any())
                return Result.Failure(
                    new List<GetBorrowedBooksByMemberIdResponse>(),
                    new Error("404", "No borrowed books found for this member")
                );

            var response = mapper.Map<List<GetBorrowedBooksByMemberIdResponse>>(filteredBooks);

            // Calculate fine for each book
            DateTime today = DateTime.Today;

            foreach (var book in response)
            {
                // Check if book hasn't been returned (ReturnDate is MinValue)
                if (book.ReturnDate == "1/1/0001" || string.IsNullOrEmpty(book.ReturnDate))  // Check if string matches DateOnly.MinValue
                {
                    // Try to parse the due date string
                    if (DateTime.TryParse(book.DueDate, out DateTime dueDateTime))
                    {
                        // Check if book is overdue
                        if (today > dueDateTime)
                        {
                            int daysOverdue = (today - dueDateTime).Days;
                            // Apply fine based on days overdue
                            var dayCount = daysOverdue > 5 ? 20 : 10;
                            book.Fine = dayCount * daysOverdue;
                        }
                    }
                }
            }

            return Result.Success(response);
        }
    }
}