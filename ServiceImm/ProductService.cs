using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;

using Shared;
using Shared.DTO.ProductDto;
using Shared.Enums;
using Shared.Prameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class ProductService(IMapper _mapper, IMainUnitOfWork _mainUnitOfWork) : IProductService
    {


        public async Task<PaginationResponse<ProductDto>> GetAllProductMainRepoAsync(ProductPrameter productPrameter)
        {
            // 1. استخدم Get من MainRepo للـ includes و filter
            var baseQuery = _mainUnitOfWork
                .GetRepository<Product,int>()
                .Get(
                    filter: p =>
                        (!productPrameter.BrandId.HasValue || p.ProductBrandId == productPrameter.BrandId.Value) &&
                        (!productPrameter.TypeId.HasValue || p.ProductTypeId == productPrameter.TypeId.Value) &&
                        (string.IsNullOrEmpty(productPrameter.Search) || p.Name.ToLower().Contains(productPrameter.Search.ToLower())),
                    includes: [p => p.ProductBrand, p => p.ProductType]
                );

            // 2. ابعت الـ query على ProductRepository علشان يضيف الـ sort
            var sortedQuery = _mainUnitOfWork.ProductRepository.GetSortedProducts(baseQuery, productPrameter.ProductSortingOptions);

            //3 -apply pagination
            var ApplyPagination = sortedQuery.Skip(productPrameter.PageIndex * productPrameter.PageSize)
                                            .Take(productPrameter.PageSize);

            // 3. نفذ الـ query
            var products = await ApplyPagination.ToListAsync();

            var Data = _mapper.Map<IEnumerable<ProductDto>>(products);
            return new PaginationResponse<ProductDto>(productPrameter.PageIndex, Data.Count(), sortedQuery.Count(), Data);
        }





        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var Brands = await _mainUnitOfWork
               .GetRepository<ProductBrand,int>().Get().ToListAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<ProductDto?> GetProductMainByIdAsync(int id)
        {
            var product = await _mainUnitOfWork
                .GetRepository<Product,int>()
                .GetOne(
                    filter: p => p.Id == id,
                    includes: [e => e.ProductBrand, e => e.ProductType]);

            if (product is null)
            {
                throw new ProductNotFoundEx(id);
            }

            return _mapper.Map<ProductDto?>(product);
        }


        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {

            var Types = await _mainUnitOfWork
                 .GetRepository<ProductType,int>().Get().ToListAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }
    }
}
