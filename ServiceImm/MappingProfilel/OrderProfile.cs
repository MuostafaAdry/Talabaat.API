using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DTO.IdentityDto;
using Shared.DTO.OrdersDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm.MappingProfilel
{
    class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(e => e.DeliveryMethod, e => e.MapFrom(e => e.DelivaryMethod.ShortName));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(e => e.ProductName, e => e.MapFrom(e => e.Product.ProductName))
                .ForMember(e => e.PictureUrl, e => e.MapFrom(e => e.Product.PictureUrl));


            CreateMap<DelivaryMethod, DelivaryMethodDto>();
            CreateMap<Order, OrderDto>();



        }
    }
}
