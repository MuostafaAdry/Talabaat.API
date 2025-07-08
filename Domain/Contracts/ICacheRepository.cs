using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        // get 
        Task<string?> GetAsync(string cacheKey);
        // set 
        Task SetAsync(string cacheKey, string value, TimeSpan timeSpan);
    }
}
