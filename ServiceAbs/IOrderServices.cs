using Shared.DTO.OrdersDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface IOrderServices
    {
        //Take [Basket Id , Shipping Address , Delivery Method Id ,] Customer Email
        //And Return Order Details (Id , UserName , OrderDate ,
        //Items (Product Name - Picture Url - Price - Quantity) ,
        //Address , Delivery Method Name , Order Status Value , Sub Total , Total Price  )
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto,string email);

        // Get All DelivaryMethodDto
        Task<IEnumerable<DelivaryMethodDto>> GetDelivaryMethodAsync();

        // GetAllOrdersAsync 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);

        // get order by Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);


    }
}
