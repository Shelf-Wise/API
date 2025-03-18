using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class GetAllBooksQueryHandler
        : IQueryHandler<GetAllBooksQuery, Result<List<GetAllBooksResultDto>>>
    {
        private readonly IGenericWriteRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IGenericWriteRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBooksResultDto>>> Handle(
            GetAllBooksQuery query,
            CancellationToken cancellationToken
        )
        {
            var books = await _repository.GetAllWithInclude(b => b.Genres);

            if (books == null)
                return Result.Failure(
                    new List<GetAllBooksResultDto>(),
                    new Error("400", "No Data Found")
                );

            return Result.Success(_mapper.Map<List<GetAllBooksResultDto>>(books));
        }
    }
}
