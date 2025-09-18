namespace BookReview.Data.Repositories.Interfaces
{
    public interface ICacheRepository
    {
        Task SetAsync(string key,string value,TimeSpan TTL);
        Task<string?> GetAsync(string key);
    }
}
