using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Customer.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class ReviewsController(IReviewService service) : ControllerBase
    {
        private readonly IReviewService _service = service;

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null) return Unauthorized();
            var result = await _service.AddReviewAsync(request, userId);
            if (!result) return BadRequest("You cannot review this product.");
            return Ok("Review added successfully.");
        }
    }
}
