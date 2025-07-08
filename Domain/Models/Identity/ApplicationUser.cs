﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public Addrerss? Addrerss { get; set; }
    }
}
