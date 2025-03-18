using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Genre.Dto;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.Genre.Query
{
    public record GetAllGenresQuery : IQuery<Result<List<GetGenreDto>>>
    {
    }
}
