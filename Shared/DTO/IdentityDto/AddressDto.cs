﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.IdentityDto
{
    public class AddressDto
    {
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string FristName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
