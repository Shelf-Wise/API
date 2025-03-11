using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagementC.Domain.Enums;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class UpdateBookCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public Category Category { get; set; }
    }
}
