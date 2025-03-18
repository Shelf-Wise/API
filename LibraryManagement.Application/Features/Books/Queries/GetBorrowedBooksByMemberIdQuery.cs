using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public record GetBorrowedBooksByMemberIdQuery : IQuery<Result<List<GetBorrowedBooksByMemberIdResponse>>>
    {
        public Guid MemberId { get; set; }
    }
}
