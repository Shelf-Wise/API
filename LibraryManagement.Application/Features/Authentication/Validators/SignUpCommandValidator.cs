using FluentValidation;
using LibraryManagement.Application.Features.Authentication.Command;

namespace LibraryManagement.Application.Features.Authentication.Validators
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role Cannot be Empty");
        }
    }
}
