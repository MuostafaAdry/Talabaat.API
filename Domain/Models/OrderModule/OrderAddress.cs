﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class OrderAddress
    {
        public string FristName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
