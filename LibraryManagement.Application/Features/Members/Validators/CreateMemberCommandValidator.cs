using FluentValidation;
using LibraryManagement.Application.Features.Members.Commands;

namespace LibraryManagement.Application.Features.Members.Validators
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Member is name is required")
                .MaximumLength(50)
                .WithMessage("Member Name cannot exceed more than 50 characters");
        }
    }
}
