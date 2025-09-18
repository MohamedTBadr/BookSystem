using BookReview.Data.Repositories.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace BookReview.Data.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : ICacheRepository
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var value= await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value;
        }

        public async Task SetAsync(string key, string value, TimeSpan TTL)
        {
            //var serializeObject = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, value, TTL);

        }
    }
}
