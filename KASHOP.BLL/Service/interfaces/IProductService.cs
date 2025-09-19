using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IProductService: IGenericService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateWithFile(ProductRequest productRequest);
        Task<List<ProductResponse>> GetAllProduct(HttpRequest httpRequest,bool onlyActive = false);
    }
}
