using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Shared.Enums;
using Shared.Prameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presistence.Repositories
{
   public class ProductRepository:MainRepo<Product,int>,IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext):base (dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> GetSortedProducts(IQueryable<Product> query, ProductSortingOptions? sort)
        {
            return sort switch
            {
                ProductSortingOptions.NameAsc => query.OrderBy(p => p.Name),
                ProductSortingOptions.Namedesc => query.OrderByDescending(p => p.Name),
                ProductSortingOptions.PriceAsc => query.OrderBy(p => p.Price),
                ProductSortingOptions.Pricedesc => query.OrderByDescending(p => p.Price),
                _ => query
            };
        }
        public async Task<List<Product>> GetPaginatedProductsAsync(ProductPrameter parameters)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .AsQueryable();
            //filer with brandId

            if (parameters.BrandId.HasValue)
                query = query.Where(p => p.ProductBrandId == parameters.BrandId.Value);
            //filer with typeId
            if (parameters.TypeId.HasValue)
                query = query.Where(p => p.ProductTypeId == parameters.TypeId.Value);
            //filer with search product name
            if (!string.IsNullOrWhiteSpace(parameters.Search))
                query = query.Where(p => p.Name.ToLower().Contains(parameters.Search.ToLower()));

            // Apply sorting
            query = GetSortedProducts(query, parameters.ProductSortingOptions);

            // Apply pagination
            query = query
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize);

            return await query.ToListAsync();
        }


       

 
    }
}
