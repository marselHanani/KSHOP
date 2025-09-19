﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        public string MainImageUrl { get; set; }
        public List<string> SubImagesUrl { get; set; }
    }
}
