using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Response;

namespace KASHOP.DAL.Repositories.@interface
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        Task DecreaseQuantity(List<(int productId, int count)> items);
        Task<List<Product>> GetAllProductsWithImages();
    }
}
