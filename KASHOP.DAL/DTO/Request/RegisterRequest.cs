﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
