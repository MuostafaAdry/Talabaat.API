using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket customerBasket, TimeSpan? timeSpan = null)
        {
            // we must serialize CustomerBasket because redis store data as json
            var JsonBasket = JsonSerializer.Serialize(customerBasket);
            var IsCreatedOrUpdated =await _database.StringSetAsync(customerBasket.Id,JsonBasket,timeSpan??TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetCustomerBasketAsync(customerBasket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string kek)
        {
           return await _database.KeyDeleteAsync(kek);
        }

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string key)
        {
            // we must Deserialize CustomerBasket because redis store data as json
            var Basket =await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
    }
}
