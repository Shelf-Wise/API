using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagement.Application.Shared;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public class RecommendationSystemQuery : IQuery<Result<List<GetAllBooksResultDto>>>
    {
        public Guid MemberId { get; set; }

        public RecommendationSystemQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}
