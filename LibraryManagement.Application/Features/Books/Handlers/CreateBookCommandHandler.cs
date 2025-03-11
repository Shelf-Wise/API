using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IGenericWriteRepository<Book> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(
            IGenericWriteRepository<Book> repository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CreateBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var _book = Book.Create(
                command.Title,
                command.Author,
                command.PublicationYear,
                command.ISBN,
                command.ImageURls
            );

            var saved = await _repository.AddEntityAsync(_book);

            await _unitOfWork.SaveChangesAsync();

            if (saved != null)
                return Result.Success("Book Created Successfully!");
            else
                return Result.Failure(new Error("400", "Unable to create book"));
        }
    }
}
