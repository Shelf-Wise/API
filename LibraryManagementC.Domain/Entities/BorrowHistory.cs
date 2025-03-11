using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class BorrowHistory: BaseEntity
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        public DateOnly BorrowedDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly ReturnDate { get; set; }

        public required Member Member { get; set; }
        public required Book Book { get; set; }
    }
}
