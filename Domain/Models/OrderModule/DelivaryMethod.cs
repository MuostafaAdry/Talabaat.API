using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class DelivaryMethod:BaseEntity<string>
    {
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DelivaryTime { get; set; } = default!;
                      
        public decimal Price { get; set; }
    }
}
