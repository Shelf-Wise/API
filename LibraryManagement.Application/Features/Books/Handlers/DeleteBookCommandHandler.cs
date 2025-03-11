using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    internal class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly IGenericWriteRepository<Book> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(
            IGenericWriteRepository<Book> repository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var book = Book.Delete(command.Id);

            var result = _repository.DeleteEntityAsync(book);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result
                ? Result.Success("Deleted Successfully")
                : Result.Failure(new Error("400", $"Was unable to Delete ID {command}"));
        }
    }
}
