using FluentValidation;
using LibraryManagement.Application.Features.Members.Commands;

namespace LibraryManagement.Application.Features.Members.Validators
{
    public class DeleteMemberCommandValidator:AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberCommandValidator() 
        {
        }
    }
}
