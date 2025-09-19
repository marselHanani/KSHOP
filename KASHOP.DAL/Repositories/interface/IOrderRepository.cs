using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrder(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum status);
        Task<List<Order>> GetByStatusAsync(OrderStatusEnum status);
        Task<List<Order>> GetByUserIdAsync(string userId);
    }
}
