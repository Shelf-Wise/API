using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Members.Dtos;
using LibraryManagement.Application.Features.Members.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Members.Handlers
{
    public class GetAllMembersQueryHandler
        : IQueryHandler<GetAllMemberQuery, Result<List<GetAllMembersResponseDto>>>
    {
        private readonly IGenericWriteRepository<Member> _repository;
        private readonly IGenericWriteRepository<BorrowHistory> _historyRepository;
        private readonly IMapper _mapper;

        public GetAllMembersQueryHandler(IGenericWriteRepository<Member> repository, IMapper mapper, IGenericWriteRepository<BorrowHistory> historyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _historyRepository = historyRepository;
        }

        public async Task<Result<List<GetAllMembersResponseDto>>> Handle(
            GetAllMemberQuery query,
            CancellationToken cancellationToken
        )
        {
            var data = await _repository.GetAllWithInclude(m => m.Books);

            //var borrowedBooks = await _historyRepository.

            if (data == null)
                return Result.Failure(
                    new List<GetAllMembersResponseDto>(),
                    new Error("400", "No Data Found")
                );

            // Map members to DTOs
            var mappedData = _mapper.Map<List<GetAllMembersResponseDto>>(data);

            // If you need to count borrowed books for each member
            foreach (var (dto, member) in mappedData.Zip(data))
            {
                dto.NoOfBooksBorrowed = member.Books.Count(b => b.Status == LibraryManagementC.Domain.Enums.BookStatus.BORROWED);
            }

            return Result.Success(mappedData);
        }
    }
}
