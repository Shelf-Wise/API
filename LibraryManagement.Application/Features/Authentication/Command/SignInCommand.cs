using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Authentication.Command
{
    public class SignInCommand : ICommand
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
