using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string ImageURls { get; set; } = string.Empty;
    }
}
