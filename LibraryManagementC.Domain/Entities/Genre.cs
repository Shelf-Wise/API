using LibraryManagementC.Domain.Enums;
using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public required string Name { get; set; }
        public required IList<Book> Books { get; set; }
    }
}
