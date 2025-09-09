using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IProductService: IGenericService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateWithFile(ProductRequest productRequest);
    }
}
