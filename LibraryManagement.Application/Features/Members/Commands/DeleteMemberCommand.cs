using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public class DeleteMemberCommand : ICommand
    {
        public Guid MemberId { get; set; }
    }
}
