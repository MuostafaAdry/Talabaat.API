using Domain.Contracts;
using ServiceAbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class CacheService(ICacheRepository _cacheRepositoey) : ICacheService
    {
        public async Task<string> GetCacheAsync(string cacheKey)
        {
            return await _cacheRepositoey.GetAsync(cacheKey);
        }

        public async Task SetCacheAsync(string cacheKey, object cachevalue, TimeSpan timeSpan)
        {
            var value = JsonSerializer.Serialize(cachevalue);
           await _cacheRepositoey.SetAsync( cacheKey, value,timeSpan);
        }
    }
}
