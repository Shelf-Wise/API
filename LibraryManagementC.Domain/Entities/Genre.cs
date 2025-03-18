using LibraryManagementC.Domain.Primitives;
using System.Text.Json.Serialization;

namespace LibraryManagementC.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public IList<Book> Books { get; set; }

        public Genre(string name)
        {
            Name = name;
        }

        public static Genre Create(string name)
        {
            return new Genre(name);
        }
    }
}
