﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.IdentityDto
{
    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
