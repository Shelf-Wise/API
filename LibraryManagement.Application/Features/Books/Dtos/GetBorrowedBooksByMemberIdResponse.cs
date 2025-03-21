using LibraryManagementC.Domain.Enums;

namespace LibraryManagement.Application.Features.Books.Dtos
{
    public class GetBorrowedBooksByMemberIdResponse
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public BookStatus BookStatus { get; set; }
        public required string ImageUrl { get; set; }
        public Guid MemberId { get; set; }
        public string DueDate { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public string CreatedAt { get; set; }
        public int Fine { get; set; }
    }
}
