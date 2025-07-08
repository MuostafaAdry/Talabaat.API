using Shared.DTO.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string key);
        Task<bool> DeleteBasketAsync(string key);
        Task<BasketDto> CreateOrUpdateAsync(BasketDto basketDto);
    }
}
