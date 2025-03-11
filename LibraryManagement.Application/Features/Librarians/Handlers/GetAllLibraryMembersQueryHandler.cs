//using AutoMapper;
//using LibraryManagement.Application.Abstractions.Messaging;
//using LibraryManagement.Application.Abstractions.Persistence;
//using LibraryManagement.Application.Features.LibraryMembers.Dtos;
//using LibraryManagement.Application.Features.LibraryMembers.Query;
//using LibraryManagement.Application.Shared;

//namespace LibraryManagement.Application.Features.LibraryMembers.Handlers
//{
//    public class GetAllLibraryMembersQueryHandler
//        : IQueryHandler<GetAllLibraryMembersQuery, Result<List<GetAllLibraryMembersDto>>>
//    {
//        private readonly ILibraryMemberReadRepository _repository;
//        private readonly IMapper _mapper;

//        public GetAllLibraryMembersQueryHandler(
//            ILibraryMemberReadRepository repository,
//            IMapper mapper
//        )
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<Result<List<GetAllLibraryMembersDto>>> Handle(
//            GetAllLibraryMembersQuery query,
//            CancellationToken cancellationToken
//        )
//        {
//            var data = await _repository.GetAllLibraryMembersAsync();

//            if (data.Count == 0)
//                return Result.Failure(
//                    new List<GetAllLibraryMembersDto>(),
//                    new Error("400", "No Data Avaialable")
//                );

//            return Result.Success(_mapper.Map<List<GetAllLibraryMembersDto>>(data));
//        }
//    }
//}
