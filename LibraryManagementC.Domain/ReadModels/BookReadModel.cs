using LibraryManagementC.Domain.Enums;
using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.ReadModels
{
    public class BookReadModel : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public int PublicationYear { get; private set; }
        public Category Category { get; private set; }
    }
}
