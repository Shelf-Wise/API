using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class Member : BaseEntity
    {

        public string FullName { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string ImageUrl { get; set; }

        private static readonly List<Book> _books = new List<Book>();
        public virtual ICollection<Book> Books { get; private set; } = new List<Book>();

        public Member() { }
        public Member(Guid Id) { this.Id = Id; }

        public Member(Guid id, string fullName, string address, string nIC, string telephone, string email, string dOB, string imageUrl)
        {
            Id = id;
            FullName = fullName;
            Address = address;
            NIC = nIC;
            Telephone = telephone;
            Email = email;
            DOB = dOB;
            ImageUrl = imageUrl;
        }

        public Member(string fullName, string address, string nIC, string telephone, string email, string dOB, string imageUrl)
        {
            FullName = fullName;
            Address = address;
            NIC = nIC;
            Telephone = telephone;
            Email = email;
            DOB = dOB;
            ImageUrl = imageUrl;
        }

        public static Member Create(string fullName, string address, string nIC, string telephone, string email, string dOB, string imageUrl)
        {
            return new Member(fullName, address, nIC, telephone, email, dOB, imageUrl);
        }

        public static Member Update(Guid id, string fullName, string address, string nIC, string telephone, string email, string dOB, string imageUrl)
        {
            return new Member(id, fullName, address, nIC, telephone, email, dOB, imageUrl);
        }

        public static Member Remove(Guid id)
        {
            return new Member(id);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBookFromborrowedList(Book book)
        {
            _books.Remove(book);
        }
    }
}
