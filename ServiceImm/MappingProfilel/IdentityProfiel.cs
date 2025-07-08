using AutoMapper;
using Domain.Models.Identity;
using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm.MappingProfilel
{
   public class IdentityProfiel:Profile
    {
        public IdentityProfiel()
        {
            CreateMap<Addrerss, AddressDto>().ReverseMap();
            
        }
    }
}
