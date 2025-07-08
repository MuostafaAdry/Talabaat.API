using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.OrdersDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string DeliveryMethodId { get; set; }
        public AddressDto shipToAddress { get; set; }
    }
}
