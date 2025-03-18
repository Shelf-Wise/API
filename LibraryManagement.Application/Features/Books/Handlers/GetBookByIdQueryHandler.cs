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
        private readonly IGenericWriteRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IGenericWriteRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetBookResultDto>> Handle(
            GetBookByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var book = await _repository.Get(query.BookId, q => q.Genres);

            if (book == null)
                return Result.Failure<GetBookResultDto>(
                    default,
                    new Error("404", "Book Id Not found")
                );

            return Result.Success(_mapper.Map<GetBookResultDto>(book));
        }
    }
}
