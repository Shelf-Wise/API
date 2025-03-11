using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Authentication.Command
{
    public class SignInCommand : ICommand
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
