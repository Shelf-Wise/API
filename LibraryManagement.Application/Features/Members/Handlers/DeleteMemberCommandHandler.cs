using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Members.Handlers
{
    public class DeleteMemberCommandHandler : ICommandHandler<DeleteMemberCommand>
    {
        private readonly IGenericWriteRepository<Member> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IGenericWriteRepository<Member> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteMemberCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteLibraryMember = Member.Remove(command.MemberId);
                _repository.DeleteEntityAsync(deleteLibraryMember);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success("Deleted Successfully!");
            }
            catch (Exception err)
            {
                return Result.Failure(new Error("500", err.Message));
            }

        }
    }
}
