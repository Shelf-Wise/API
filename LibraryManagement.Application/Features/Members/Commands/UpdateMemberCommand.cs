using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public class UpdateMemberCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Dob { get; set; }
        public string Nic { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
