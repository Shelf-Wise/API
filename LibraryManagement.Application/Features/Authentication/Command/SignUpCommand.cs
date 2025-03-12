using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Authentication.Command
{
    public class SignUpCommand : ICommand
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
