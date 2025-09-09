using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KASHOP.PL.Areas.Customer.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpPost("")]
        public IActionResult AddToCart([FromBody] CartRequest request)
        {
            var userId = User.FindFirstValue("Id");
            var result = _service.AddToCart(request, userId);
            return result.Result ? Ok("Item added to cart") : BadRequest("Failed to add item to cart");
        }

        [HttpGet("")]
        public async Task<IActionResult> CartSummary()
        {
            var userId = User.FindFirstValue("Id");
            var response = await _service.CartSummaryResponse(userId);
            return Ok(response);
        }
    }
}
