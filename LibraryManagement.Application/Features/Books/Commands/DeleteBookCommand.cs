using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class DeleteBookCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
