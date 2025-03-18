using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Members.Handlers
{
    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand>
    {
        private readonly IGenericWriteRepository<Member> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IGenericWriteRepository<Member> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
        {
            var member = Member.Create(
                command.FullName,
                command.Address,
                command.NIC,
                command.Telephone,
                command.Email,
                command.DOB,
                command.ImageUrl
            );

            await _repository.AddEntityAsync(member);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Member Created Successfully");

        }
    }
}
