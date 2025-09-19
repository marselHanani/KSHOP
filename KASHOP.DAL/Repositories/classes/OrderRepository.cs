using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
           var result= await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrder(int orderId)
        {
            return await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatusEnum status)
        {
            return await _context.Orders.Where((o => o.Status == status)).OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }   
        
        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order is null) return false;
            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
