using FluentValidation;
using LibraryManagement.Application.Features.Books.Commands;

namespace LibraryManagement.Application.Features.Books.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title IS Required")
                .MaximumLength(100)
                .WithMessage("Title Cannot Exceed 50 Characters");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("Author Is Required")
                .MaximumLength(100)
                .WithMessage("Author Name Cannot be More than 100 characters");

            RuleFor(x => x.PublicationYear)
                .NotEmpty()
                .WithMessage("Publication Year Cannot be Null")
                .ExclusiveBetween(1800, 2024)
                .WithMessage("Publication Year Must be 1800 to 2024");
        }
    }
}
