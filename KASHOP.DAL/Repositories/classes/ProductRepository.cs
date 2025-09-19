using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.@interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Response;

namespace KASHOP.DAL.Repositories.classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseQuantity(List<(int productId, int count)> items)
        {
          var productIds = items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach(var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if(product.Quantity < item.count)
                {
                    throw new InvalidOperationException($"Insufficient stock for product ID {product.Id}. Available: {product.Quantity}, Requested: {item.count}");
                }
                product.Quantity -= item.count;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsWithImages()
        {
            var products = await _context.Products
                .Include(p => p.subImages).ToListAsync();
            return products;
        }
    }
}
