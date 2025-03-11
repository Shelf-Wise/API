using AutoMapper;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Shared;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Features.Books.Handlers
{
    public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, Result<GetBookResultDto>>
    {
        private readonly IGenericReadRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IGenericReadRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetBookResultDto>> Handle(
            GetBookByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var book = await _repository.GetByIdAsync(query.BookId, "Books");

            if (book == null)
                return Result.Failure<GetBookResultDto>(
                    default,
                    new Error("404", "Book Id Not found")
                );

            return Result.Success(_mapper.Map<GetBookResultDto>(book));
        }
    }
}
