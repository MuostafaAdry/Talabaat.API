﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BadRequestException(List<string> errors):Exception("Validation Faild")
    {
        public List<string> Errors { get; set; } = errors;
    }
}
