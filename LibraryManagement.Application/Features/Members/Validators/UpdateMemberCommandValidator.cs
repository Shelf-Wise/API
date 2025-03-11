using FluentValidation;
using LibraryManagement.Application.Features.Members.Commands;

namespace LibraryManagement.Application.Features.Members.Validators
{
    public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Member Name cannot be Empty")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");
        }
    }
}
