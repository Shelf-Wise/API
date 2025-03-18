using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Genre.Command;
using LibraryManagement.Application.Shared;
using DomainEntites = LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Genre.Handlers
{
    public class CreateGenreCommandHandler : ICommandHandler<CreateGenreCommand>
    {
        private readonly IGenericWriteRepository<DomainEntites.Genre> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateGenreCommandHandler(IGenericWriteRepository<DomainEntites.Genre> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = DomainEntites.Genre.Create(command.Name);
            await repository.AddEntityAsync(genre);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Genre Created");
        }
    }
}
