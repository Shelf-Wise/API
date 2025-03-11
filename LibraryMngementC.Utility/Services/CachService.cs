using LibraryManagement.Application.Abstractions.Utility;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryMngementC.Utility.Services
{
    public class CachService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly TimeSpan DefaultExpirationTime = TimeSpan.FromMinutes(5);

        public CachService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<T?> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            T? result = await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiration ?? DefaultExpirationTime);
                    return factory(cancellationToken);
                });

            return result;
        }
    }
}
