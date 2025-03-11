using LibraryManagementC.Domain.Enums;
using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class Librarian : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public Librarian() { }

        public Librarian(Guid id, string name)
        {
            Id = id;
            Name = name ?? string.Empty;
        }

        public Librarian(Guid id)
        {
            Id = id;
        }

        public Librarian(string name)
        {
            Name = name;
        }
    }
}
