using BookReview.Data.Repositories.Interfaces;
using BookReview.Services.Interfaces;

namespace BookReview.Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public  async Task<string?> GetCachedValueAsync(string cacheKey)=>await cacheRepository.GetAsync(cacheKey);


        public async Task SetCacheValueAsync(string CacheKey, object value, TimeSpan TTL)=> await cacheRepository.SetAsync(CacheKey, (string)value, TTL);
    }
}
