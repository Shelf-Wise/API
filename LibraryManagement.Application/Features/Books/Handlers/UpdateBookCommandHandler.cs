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
            CancellationToken cancellationToken
        )
        {
            var _book = await _repository.Get(command.Id);

            if(_book == null)
            {
                return Result.Failure(new Error("400", "Book not found"));
            }

            _book.Title=command.Title;
            _book.Author=command.Author;
            _book.PublicationYear = command.PublicationYear;
            _book.ISBN = command.ISBN;
            _book.ImageURL = command.imageUrl;

            _book.Genres.Clear();

            if (command.GenreIds != null && command.GenreIds.Any())
            {
                var genres = new List<DomainEntities.Genre>();
                foreach (var genreId in command.GenreIds)
                {

                    // Then add the new genres
                    foreach (var genre in genres)
                    {
                       
                        _book.Genres.Add(genre);
                    }
                }

                // Add all found genres to the book
                foreach (var genre in genres)
                {
                    _book.Genres.Add(genre);
                }
            }

            var saved = _repository.UpdateEntityAsync(_book);

            await _unitOfWork.SaveChangesAsync();

            if (saved != null)
                return Result.Success("Book Updated Successfully!");
            else
                return Result.Failure(new Error("400", "Unable to create book"));
        }
    }
}
