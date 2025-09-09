﻿using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IBrandService : IGenericService<BrandRequest,BrandResponse, Brand>
    {
        Task<int> CreateWithFile(BrandRequest request);
    }
}
