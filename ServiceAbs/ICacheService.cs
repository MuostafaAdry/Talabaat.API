using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface ICacheService
    {
        // get
        Task<string> GetCacheAsync(string cacheKey);
        // set 
        Task SetCacheAsync(string cacheKey,object value, TimeSpan timeSpan);
    }
}
