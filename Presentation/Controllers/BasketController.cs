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
    public class BasketController(IServiceManager _serviceManager) :ControllerBase
    {
        // get basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket =await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }

        // delete basket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result =await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }

        // create or update 
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(BasketDto basketDto)
        {
            var Basket =await _serviceManager.BasketService.CreateOrUpdateAsync(basketDto);
            return Ok(Basket);
        }

    }
}
