using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public class GetAllBooksQuery : IQuery<Result<List<GetAllBooksResultDto>>> { }
}
