using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCart(CartRequest request, string userId);
        Task<CartSummaryResponse> CartSummaryResponse(string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
