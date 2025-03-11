using LibraryManagementC.Domain.Enums;
using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int PublicationYear { get; private set; }
        public string ISBN { get; set; }
        public BookStatus Status { get; set; }
        public string ImageURL { get; set; }
        public Guid? MemberId { get; set; } 
        public Member? Member { get; set; }
        public IList<Genre> Genres { get; set; } = new List<Genre>();

        public Book() { }
        public Book(Guid id)
        {
            Id = id;
        }
        public Book(string title,
                    string author,
                    int publicationYear,
                    string iSBN,
                    BookStatus status,
                    string imageURL,
                    IList<Genre> genres)
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            Status = status;
            ImageURL = imageURL;
            PublicationYear = publicationYear;
            Genres = genres;
        }
        public Book(string title,
                    string author,
                    int publicationYear,
                    string iSBN,
                    BookStatus status,
                    string imageURL
            )
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            Status = status;
            ImageURL = imageURL;
            PublicationYear = publicationYear;
        }

        public static Book Create(string title, string author, int publicationYear, string iSBN, string imageURL)
        {            
            Book Book = new(title, author, publicationYear, iSBN, BookStatus.AVAILABLE, imageURL);
            return Book;
        }

        public static Book Update(string title, string author, int publicationYear, IList<Genre> genres, string iSBN, BookStatus status, string imageURL)
            { return new Book(title, author, publicationYear, iSBN, status, imageURL, genres); }


        public static Book Delete(Guid id)
        { return new Book(id); }
    }
}
