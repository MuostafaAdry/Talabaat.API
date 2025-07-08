using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbs;
using Shared.DTO.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class PaymentController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePayment(string BasketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentAsync(BasketId);
            return Ok(Basket);
        }


        



    }
}
