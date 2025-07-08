using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbs;
using Shared;
using Shared.DTO.ProductDto;
using Shared.Enums;
using Shared.Prameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Cache]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        // get all products
        [HttpGet("AllProducts")]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllMainProducts([FromQuery] ProductPrameter productPrameter)
        {
             var Products = await _serviceManager.ProductService.GetAllProductMainRepoAsync(productPrameter);
            return Ok(Products);
        }

        // get  product id
        //[HttpGet("{id}GetProductId")]
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductMainId([FromRoute]int id)
        {
            var product =await _serviceManager.ProductService.GetProductMainByIdAsync(id);
            return Ok(product);
        }
        //get all brands
        [HttpGet("AllBrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands =await _serviceManager.ProductService.GetBrandsAsync();
            return Ok(Brands);
        }

        //get all types
        [HttpGet("AllTypes")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllTypes()
        {
            var Types = await _serviceManager.ProductService.GetTypesAsync();
            return Ok(Types);
        }
    }
}
