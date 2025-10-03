using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.DAL.Repositories.classes
{
    public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<bool> UserHasReviewedProduct(string userId, int productId)
        {
            return _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public Task AddReviewAsync(Review review, string userId)
        {
            review.UserId = userId;
            _context.Reviews.Add(review);
            return _context.SaveChangesAsync();
        }
    }
}
