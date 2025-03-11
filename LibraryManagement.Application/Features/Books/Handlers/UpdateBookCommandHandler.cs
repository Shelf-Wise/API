//using LibraryManagement.Application.Abstractions.Messaging;
//using LibraryManagement.Application.Abstractions.Persistence;
//using LibraryManagement.Application.Features.Books.Commands;
//using LibraryManagement.Application.Shared;
//using LibraryManagementC.Domain.Entities;

//namespace LibraryManagement.Application.Features.Books.Handlers
//{
//    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
//    {
//        private readonly IGenericWriteRepository<Book> _repository;
//        private readonly IUnitOfWork _unitOfWork;

//        public UpdateBookCommandHandler(IGenericWriteRepository<Book> repository, IUnitOfWork unitOfWork)
//        {
//            _repository = repository;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<Result> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
//        {
//            Book newBook = Book.Update(command.Id, command.Title, command.Author, command.PublicationYear, command.Category);
//            var result = _repository.UpdateEntityAsync(newBook);

//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return result ? Result.Success(result) : Result.Failure(new Error("400", $" {command.Id} Unable to update"));

//        }
//    }
//}
