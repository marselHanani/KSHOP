using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service.interfaces;

public interface IOrderService
{
    Task<Order?> GetUserByOrder(int orderId);
    Task<Order?> AddAsync(Order order);
    Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum status);
    Task<List<Order>> GetByStatusAsync(OrderStatusEnum status);
    Task<List<Order>> GetByUserIdAsync(string userId);
}