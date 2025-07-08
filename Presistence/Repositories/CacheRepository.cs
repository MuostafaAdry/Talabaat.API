
using Domain.Contracts;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _connection) : ICacheRepository
    {
        readonly IDatabase _database = _connection.GetDatabase();
        public async Task<string?> GetAsync(string cacheKey)
        {
            var cacheValue =await _database.StringGetAsync(cacheKey);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan timeSpan)
        {
           await _database.StringSetAsync(cacheKey, value, timeSpan);
        }
    }
}
