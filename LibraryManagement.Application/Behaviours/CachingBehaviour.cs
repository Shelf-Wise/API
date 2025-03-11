using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Abstractions.Utility;
using MediatR;

namespace LibraryManagement.Application.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
    {
        private readonly ICacheService _cacheServices;

        public CachingBehaviour(ICacheService cacheServices)
        {
            _cacheServices = cacheServices;
        }

        public async Task<TResponse?> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            return await _cacheServices.GetOrCreateAsync(
                request.Key,
                _ => next(),
                request.Expiration,
                cancellationToken
            );
        }
    }
}
