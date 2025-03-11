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
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Features.LibraryMembers.Handlers
{
    internal class ReturnBookCommandHandler : ICommandHandler<ReturnBookCommand>
    {
        private readonly IGenericWriteRepository<Member> _libraryWriteRepo;
        private readonly IGenericWriteRepository<BorrowHistory> _borrowHistoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReturnBookCommandHandler(
            IGenericWriteRepository<Member> libraryWriteRepo,
            IGenericWriteRepository<BorrowHistory> borrowHistoryRepo,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMapper mapper
        )
        {
            _libraryWriteRepo = libraryWriteRepo;
            _borrowHistoryRepo = borrowHistoryRepo;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            ReturnBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var returnedBooks = await _borrowHistoryRepo.GetAllWithInclude(b => b.Book, m => m.Member);

            var returnedBook = returnedBooks.Where(b => command.BookID == b.BookId && b.MemberId == command.MemberId).FirstOrDefault();

            if (returnedBook == null)
                return Result.Failure(
                    new Error(
                        $"404",
                        $"Book was not borrowed to return"
                    )
                );

            if(returnedBook.Book.Status == BookStatus.AVAILABLE)
                return Result.Failure(
                    new Error(
                        $"404",
                        $"Book was not borrowed to return"
                    )
                );

            returnedBook.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            returnedBook.Book.Status = BookStatus.AVAILABLE;
            //returnedBook.MemberId = Guid.Empty;
            // returnedBook.MemberId = Guid.Empty;

            //add logic if book was late fine charge later

            returnedBook.Member.RemoveBookFromborrowedList(returnedBook.Book);

            _borrowHistoryRepo.UpdateEntityAsync(returnedBook);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Successfully Returned");
        }
    }
}
