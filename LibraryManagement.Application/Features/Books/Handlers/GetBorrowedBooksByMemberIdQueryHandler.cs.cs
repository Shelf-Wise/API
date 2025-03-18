using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;
using MediatR;

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

            var filteredBooks = borrowedBooks.Where(b => b.MemberId == query.MemberId);

            return Result.Success(mapper.Map<List<GetBorrowedBooksByMemberIdResponse>>(filteredBooks));
        }
    }
}