//using AutoMapper;
//using LibraryManagement.Application.Abstractions.Messaging;
//using LibraryManagement.Application.Abstractions.Persistence;
//using LibraryManagement.Application.Features.LibraryMembers.Dtos;
//using LibraryManagement.Application.Features.LibraryMembers.Query;
//using LibraryManagement.Application.Shared;

//namespace LibraryManagement.Application.Features.LibraryMembers.Handlers
//{
//    public class GetLibraryMemberQueryHandler : IQueryHandler<GetLibraryMemberQuery, Result<GetLibraryMemberDto>>
//    {
//        private readonly ILibraryMemberReadRepository _libraryMemberReadRepository;
//        private readonly IMapper _mapper;

//        public GetLibraryMemberQueryHandler(ILibraryMemberReadRepository libraryMemberReadRepository, IMapper mapper)
//        {
//            _libraryMemberReadRepository = libraryMemberReadRepository;
//            _mapper = mapper;
//        }
//        public async Task<Result<GetLibraryMemberDto>> Handle(GetLibraryMemberQuery query, CancellationToken cancellationToken)
//        {
//            var member = await _libraryMemberReadRepository.GetLibraryMemberByIdAsync(query.MemberId);

//            if (member == null)
//                return Result.Failure<GetLibraryMemberDto>(null, new Error("400", "Member Cannot be found"));

//            return Result.Success(_mapper.Map<GetLibraryMemberDto>(member));
//        }
//    }
//}
