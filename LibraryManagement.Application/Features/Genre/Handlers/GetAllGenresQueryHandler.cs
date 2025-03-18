using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Genre.Dto;
using LibraryManagement.Application.Features.Genre.Query;
using LibraryManagement.Application.Shared;
using DomainEntities = LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Genre.Handlers
{
    public class GetAllGenresQueryHandler : IQueryHandler<GetAllGenresQuery, Result<List<GetGenreDto>>>
    {
        private readonly IGenericWriteRepository<DomainEntities.Genre> _repository;
        private readonly IMapper mapper;

        public GetAllGenresQueryHandler(IGenericWriteRepository<DomainEntities.Genre> repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetGenreDto>>> Handle(GetAllGenresQuery query, CancellationToken cancellationToken)
        {
            var genres = await _repository.GetAll();
            return Result.Success(mapper.Map<List<GetGenreDto>>(genres));
        }
    }
}
