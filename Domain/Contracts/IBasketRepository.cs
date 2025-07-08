using Domain.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket customerBasket,TimeSpan? timeSpan=null);
        Task<CustomerBasket?> GetCustomerBasketAsync(string key);
        Task<bool> DeleteBasketAsync(string kek);
    }
}
