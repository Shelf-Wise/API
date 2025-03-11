using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Members.Dtos;
using LibraryManagement.Application.Features.Members.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Members.Handlers
{
    internal class GetMemberByIdQueryHandler
        : IQueryHandler<GetMemberByIdQuery, Result<GetMemberResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericReadRepository<Member> _repository;

        public GetMemberByIdQueryHandler(IMapper mapper, IGenericReadRepository<Member> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<GetMemberResponseDto>> Handle(
            GetMemberByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var data = await _repository.GetByIdAsync(query.id, "Members");

            if (data == null)
                return Result.Failure<GetMemberResponseDto>(
                    default,
                    new Error("400", "No Data Found")
                );

            return Result.Success(_mapper.Map<GetMemberResponseDto>(data));
        }
    }
}
