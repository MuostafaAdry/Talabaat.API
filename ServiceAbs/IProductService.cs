
using Shared;
using Shared.DTO.ProductDto;
using Shared.Enums;
using Shared.Prameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IProductService
    {
         
        Task<PaginationResponse<ProductDto>> GetAllProductMainRepoAsync(ProductPrameter productPrameter);
        
        Task<ProductDto> GetProductMainByIdAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();
      

    }
}
