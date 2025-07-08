using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServiceAbs;
using Shared.DTO.IdentityDto;
using Shared.DTO.OrdersDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class OrderServices(IMapper _mapper,IBasketRepository _basketRepository
        ,IMainUnitOfWork _unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email)
        {
            // we need to provide==>   [userEmail, OrderAddress address, 
            // delivaryMethodId, DelivaryMethod , ICollection<OrderItem> items,  subTotal ]
            //1- get address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.shipToAddress);
            //2- get basket

      


            //2- create order
            List<OrderItem> OrderItems = [];
            // to create order we must get order from basket 
            // get basket
            var Basket = await _basketRepository.GetCustomerBasketAsync(orderDto.BasketId) ??
                throw new BasketNotFound(orderDto.BasketId);

            // before create order we need to insure in there is order with PaymentIntentId
            var InsureOrder =await _unitOfWork.GetRepository<Order, Guid>().Get(e => e.PaymentIntentId == Basket.PaymentIntentId)
                .FirstOrDefaultAsync();
            if (InsureOrder!=null)
            {
                  _unitOfWork.GetRepository<Order, Guid>().Delete(InsureOrder);
            }
            foreach (var item in Basket.Items)
            {
                var ProductFroPrice = await _unitOfWork.GetRepository<Product, int>().GetOne(e=>e.Id==item.Id)
                    ?? throw new  ProductNotFoundEx(item.Id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrderd()
                    {
                        ProductId = item.Id,
                        ProductName = item.ProductName,
                        PictureUrl = item.PictureUrl,
                    },


                     // we need to get product to get Real price
                     Price= ProductFroPrice.Price,
                     Quantity=item.Quantaty
                     
                }; 
                OrderItems.Add(orderItem);
            }
            // get delevary method
            var DelivaryMethod = await _unitOfWork.GetRepository<DelivaryMethod,string>().GetOne(e => e.Id == orderDto.DeliveryMethodId)
                               ?? throw new DelivaryMethodException(orderDto.DeliveryMethodId);

            var SubTotal = OrderItems.Sum(e => e.Quantity * e.Price);

            ArgumentNullException.ThrowIfNull(Basket.PaymentIntentId);
            var Order = new Order(email, OrderAddress, DelivaryMethod, OrderItems, SubTotal, Basket.PaymentIntentId);
            await _unitOfWork.GetRepository<Order,Guid>().Create(Order);
             await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var order =await _unitOfWork.GetRepository<Order, Guid>()
                .Get(e => e.BuyerEmail == email, includes: [e => e.DelivaryMethod, e => e.Items]).ToListAsync()
                        ?? throw new OrderEmailNotFoundException(email);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(order);
        }

        public async Task<IEnumerable<DelivaryMethodDto>> GetDelivaryMethodAsync()
        {
            var Delivarymethod =await  _unitOfWork.GetRepository<DelivaryMethod, string>().Get().ToListAsync();
            return _mapper.Map<IEnumerable<DelivaryMethod>, IEnumerable<DelivaryMethodDto>>(Delivarymethod);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var order =await _unitOfWork.GetRepository<Order, Guid>().GetOne(e => e.Id == id, includes: [e => e.DelivaryMethod, e => e.Items])
                         ?? throw new OrderNotFoundException(id);

           
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
