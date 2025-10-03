using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;
using Mapster;

namespace KASHOP.BLL.Service.classes
{
    public class ReviewService(IOrderRepository orderReop, IReviewRepository reviewRepo) : IReviewService
    {
        private readonly IOrderRepository _orderReop = orderReop;
        private readonly IReviewRepository _reviewRepo = reviewRepo;

        public async Task<bool> AddReviewAsync(ReviewRequest request, string userId)
        {
            var hasOrder = await _orderReop.UserHasApprovedOrderForProduct(userId, request.ProductId);
            if (!hasOrder) return false;
            var alreadyReviews = await _reviewRepo.UserHasReviewedProduct(userId, request.ProductId);
            if (!alreadyReviews) return false;
            var review = request.Adapt<Review>();
            await _reviewRepo.AddReviewAsync(review, userId);
            return true;
        }
    }
}
