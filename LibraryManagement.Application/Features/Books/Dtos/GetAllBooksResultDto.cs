using LibraryManagementC.Domain.Enums;

namespace LibraryManagement.Application.Features.Books.Dtos
{
    public class GetAllBooksResultDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public BookStatus Status { get; set; }
    }
}
