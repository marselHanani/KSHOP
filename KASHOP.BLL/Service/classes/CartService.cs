using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.@interface;
using KASHOP.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> AddToCart(CartRequest request, string userId)
        {
            var newItem = new Cart
            {
                UserId = userId,
                ProductId = request.ProductId,
                Count = 1
            };
            return await _repo.Add(newItem) > 0;
        }

        public async Task<CartSummaryResponse> CartSummaryResponse(string userId)
        {
            var cartItems = await _repo.GetUserCart(userId);
            var response = new CartSummaryResponse
            {
                 Items = cartItems.Select(item => new CartResponse
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Count = item.Count,
                    Price = item.Product.Price,
                }).ToList()
            };
            return response;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _repo.ClearCartAsync(userId);
        }
    }
}
