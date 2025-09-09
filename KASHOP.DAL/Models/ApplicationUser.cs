using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string? CodeResetPassword { get; set; }

        public DateTime? ExpireCodeResetPassword { get; set; }
    }
}
