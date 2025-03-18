using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Shared;
using DomainEntities = LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IGenericWriteRepository<DomainEntities.Book> _repository;
        private readonly IGenericWriteRepository<DomainEntities.Genre> _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(
            IGenericWriteRepository<DomainEntities.Book> repository,
            IGenericWriteRepository<DomainEntities.Genre> genreRepository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
    UpdateBookCommand command,
    CancellationToken cancellationToken)
        {
            // Get the book with its genres included
            var book = await _repository.GetWithInclude(command.Id, "Genres");
            if (book == null)
            {
                return Result.Failure(new Error("400", "Book not found"));
            }

            // Update basic properties
            book.Title = command.Title;
            book.Author = command.Author;
            book.PublicationYear = command.PublicationYear;
            book.ISBN = command.ISBN;
            book.ImageURL = command.imageUrl;

            // Instead of trying to modify the collection, let's use direct SQL
            // to manage the relationships (this avoids EF Core tracking issues)

            // First, update the book entity
            _repository.UpdateEntityAsync(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Then, handle the genre relationships directly with SQL
            // 1. Remove all existing genre relationships
            await _repository.ExecuteSqlRaw(
                "DELETE FROM \"BookGenre\" WHERE \"BooksId\" = {0}", command.Id);

            // 2. Add the new genre relationships
            if (command.GenreIds != null && command.GenreIds.Any())
            {
                foreach (var genreId in command.GenreIds)
                {
                    // Check if the genre exists
                    var genre = await _genreRepository.Get(genreId);
                    if (genre != null)
                    {
                        // Add the relationship with SQL
                        await _repository.ExecuteSqlRaw(
                            "INSERT INTO \"BookGenre\" (\"BooksId\", \"GenresId\") VALUES ({0}, {1})",
                            command.Id, genreId);
                    }
                }
            }

            return Result.Success("Book Updated Successfully!");
        }
    }
}
