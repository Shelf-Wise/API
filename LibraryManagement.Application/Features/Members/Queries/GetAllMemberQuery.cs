using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Members.Dtos;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.Members.Queries
{
    public class GetAllMemberQuery : IQuery<Result<List<GetAllMembersResponseDto>>>
    {
    }
}
