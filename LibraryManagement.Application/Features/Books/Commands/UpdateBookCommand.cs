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
        public string ISBN { get; set; }
        public string imageUrl { get; set; } = string.Empty;
        public IEnumerable<Guid> GenreIds { get; set; } = new List<Guid>();

    }
}
