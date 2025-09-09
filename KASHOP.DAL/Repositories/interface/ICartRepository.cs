using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interface
{
    public interface ICartRepository
    {
        Task<int> Add(Cart cart);
        Task<List<Cart>> GetUserCart(string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
