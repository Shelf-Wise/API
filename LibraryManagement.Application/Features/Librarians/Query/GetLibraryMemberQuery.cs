using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.LibraryMembers.Dtos;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.LibraryMembers.Query
{
    public record class GetLibraryMemberQuery(Guid MemberId) : IQuery<Result<GetLibraryMemberDto>>
    {
    }
}
