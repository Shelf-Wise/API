using FluentValidation;
using LibraryManagement.Application.Features.Authentication.Command;

namespace LibraryManagement.Application.Features.Authentication.Validators
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role Cannot be Empty");
        }
    }
}
