using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Shared;
using DomainEntities = LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IGenericWriteRepository<DomainEntities.Book> _repository;
        private readonly IGenericWriteRepository<DomainEntities.Genre> _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(
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
            CreateBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var _book = DomainEntities.Book.Create(
                command.Title,
                command.Author,
                command.PublicationYear,
                command.ISBN,
                command.imageUrl
            );


            if (command.GenreIds != null && command.GenreIds.Any())
            {
                var genres = new List<DomainEntities.Genre>();
                foreach (var genreId in command.GenreIds)
                {
                    var genre = await _genreRepository.Get(genreId);
                    if (genre != null)
                    {
                        genres.Add(genre);
                    }
                }

                // Add all found genres to the book
                foreach (var genre in genres)
                {
                    _book.Genres.Add(genre);
                }
            }

            var saved = await _repository.AddEntityAsync(_book);

            await _unitOfWork.SaveChangesAsync();

            if (saved != null)
                return Result.Success("Book Created Successfully!");
            else
                return Result.Failure(new Error("400", "Unable to create book"));
        }
    }
}
