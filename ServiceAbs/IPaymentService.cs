using Shared.DTO.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId);

        
    }
}
