using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DTO.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm.MappingProfilel
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.productBrand, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(des => des.productType, options => options.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}
