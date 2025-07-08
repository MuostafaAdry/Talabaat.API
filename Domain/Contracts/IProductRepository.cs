using Domain.Models.ProductModule;
using Shared.Enums;
using Shared.Prameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IProductRepository:IMainRepo<Product,int>
    {


        public IQueryable<Product> GetSortedProducts(IQueryable<Product> query, ProductSortingOptions? sort);
        Task<List<Product>> GetPaginatedProductsAsync(ProductPrameter parameters);
    }
}
