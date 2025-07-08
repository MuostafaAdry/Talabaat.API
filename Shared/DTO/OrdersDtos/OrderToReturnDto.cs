using Shared.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.OrdersDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto shipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal deliveryCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
    }
}
