using FluentValidation;
using LibraryManagement.Application.Features.LibraryMembers.Commands;

namespace LibraryManagement.Application.Features.LibraryMembers.Validators
{
    internal class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member Id Cannot be Empty");
            RuleFor(x => x.BookID).NotEmpty().WithMessage("Book Id Cannot be Empty");
        }
    }
}
