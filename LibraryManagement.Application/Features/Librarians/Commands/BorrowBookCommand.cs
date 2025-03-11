using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.LibraryMembers.Commands
{
    public class BorrowBookCommand : ICommand
    {
        public Guid BookID { get; set; }
        public Guid MemberId { get; set; }
    }
}
