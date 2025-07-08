using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbs;
using Shared.DTO.OrdersDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //3328BC75-060E-40E5-85A9-08DDB968ED1A
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpPost("CreateOrder")]
         
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var ProductCretaed =await _serviceManager.OrderServices.CreateOrder(orderDto, email);
            return Ok(ProductCretaed);
        }

        // get delivary Method
        [HttpGet("GetDelivaryMethod")]
        public async Task<ActionResult<IEnumerable<DelivaryMethodDto>>> GetDelivaryMethod()
        {
            var DelivaryMethods =await _serviceManager.OrderServices.GetDelivaryMethodAsync();
            return Ok(DelivaryMethods);
        }

        //GetAllOrders
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AllOrders = await _serviceManager.OrderServices.GetAllOrdersAsync(email);
            return Ok(AllOrders);
        }

        //GetOrderById
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Order =await _serviceManager.OrderServices.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
