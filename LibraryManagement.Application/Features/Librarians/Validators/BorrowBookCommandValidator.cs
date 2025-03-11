using FluentValidation;
using LibraryManagement.Application.Features.LibraryMembers.Commands;

namespace LibraryManagement.Application.Features.LibraryMembers.Validators
{
    internal class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member Id Cannot be Empty");
            RuleFor(x => x.BookID).NotEmpty().WithMessage("Book Id Cannot be Empty");
        }
    }
}
