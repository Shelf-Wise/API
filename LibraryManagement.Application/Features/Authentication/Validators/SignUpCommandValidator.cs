using FluentValidation;
using LibraryManagement.Application.Features.Authentication.Command;

namespace LibraryManagement.Application.Features.Authentication.Validators
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("User Cannot be Empty");
        }
    }
}
