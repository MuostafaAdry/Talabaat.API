using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceAbs;
using Shared.DTO.BasketDtos;
using StackExchange.Redis;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class PaymentService(IConfiguration _configuration,
        IBasketRepository _basketRepository,IMainUnitOfWork _unitOfWork
        ,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId)
        {
            // configure stripe 
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];
            // get basket by basket id
            var Basket =await _basketRepository.GetCustomerBasketAsync(basketId)?? throw new BasketNotFound(basketId);
            // get amount= get product + delivary method cost 
            foreach (var item in Basket.Items)
            {
                //get product
                var product =await _unitOfWork.GetRepository<Domain.Models.ProductModule.Product, int>()
                    .Get(e=>e.Id==item.Id).FirstOrDefaultAsync()??
                    throw new ProductNotFoundEx(item.Id);
                item.Price = product.Price;

            }
            // delivary method cost 
            var DeliveryMethod = await _unitOfWork.GetRepository<DelivaryMethod, string>().Get(e=>e.Id==Basket.DeliveryMethodId)
                .FirstOrDefaultAsync()??throw new DelivaryMethodException(Basket.DeliveryMethodId);
            Basket.ShippingPrice = DeliveryMethod.Price;

            var BasketAmount =(long)(Basket.Items.Sum(e => e.Quantaty * e.Price) + DeliveryMethod.Price)* 100;

            // create or update cost
            var PaymentService = new PaymentIntentService();
            if (Basket.PaymentIntentId is null)//create
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount= BasketAmount,
                    Currency="USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent =await PaymentService.CreateAsync(option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else// update
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount
                };
               await PaymentService.UpdateAsync(Basket.PaymentIntentId, option);
            }


             await _basketRepository.CreateOrUpdateAsync(Basket);
             return _mapper.Map<CustomerBasket, BasketDto>(Basket);


              
        }


       
    }
}
 