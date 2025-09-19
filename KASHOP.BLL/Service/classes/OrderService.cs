using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;

namespace KASHOP.BLL.Service.classes;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }
    public async Task<Order?> GetUserByOrder(int orderId)
    {
        return await _repo.GetUserByOrder(orderId);
    }

    public async Task<Order?> AddAsync(Order order)
    {
        return await _repo.AddAsync(order);
    }

    public async Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum status)
    {
        return await _repo.ChangeStatusAsync(orderId, status);
    }

    public async Task<List<Order>> GetByStatusAsync(OrderStatusEnum status)
    {
        return  await _repo.GetByStatusAsync(status);
    }

    public async Task<List<Order>> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
}