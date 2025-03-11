using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public class CreateMemberCommand : ICommand
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string DOB { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
    }
}
