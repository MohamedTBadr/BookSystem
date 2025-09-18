namespace BookReview.Services.Interfaces
{
    public interface ICacheService
    {
        public Task<string?> GetCachedValueAsync(string cacheKey);
        public Task SetCacheValueAsync(string CacheKey,object value,TimeSpan TTL);

    }
}
