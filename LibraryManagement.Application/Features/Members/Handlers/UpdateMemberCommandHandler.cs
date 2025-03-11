using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Members.Handlers
{
    public class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
    {
        private readonly IGenericWriteRepository<Member> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(
            IGenericWriteRepository<Member> repository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateMemberCommand command,
            CancellationToken cancellationToken
        )
        {
            var member = Member.Update(
                command.Id,
                command.FullName,
                command.Address,
                command.Nic,
                command.Telephone,
                command.Email,
                command.Dob
            );

            _repository.UpdateEntityAsync(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Updated Successfully!");
        }
    }
}
