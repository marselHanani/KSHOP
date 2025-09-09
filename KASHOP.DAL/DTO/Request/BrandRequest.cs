using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Request
{
    public class BrandRequest
    {
        public string Name {  get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
