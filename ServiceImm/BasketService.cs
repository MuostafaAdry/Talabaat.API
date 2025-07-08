using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServiceAbs;
using Shared.DTO.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class BasketService( IMapper _mapper,IBasketRepository _basketRepository) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basketDto)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basketDto);
            var CreatedUpdatedBasket= _basketRepository.CreateOrUpdateAsync(CustomerBasket);
            if (CreatedUpdatedBasket != null)
                return await GetBasketAsync(basketDto.Id);
            else
                throw  new Exception("Can Not Create Or Update this Basket");

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket =await _basketRepository.GetCustomerBasketAsync(key);
            if (basket != null)
               return _mapper.Map<CustomerBasket, BasketDto>(basket);
            else
                throw new BasketNotFound(key);
        }
    }
}
