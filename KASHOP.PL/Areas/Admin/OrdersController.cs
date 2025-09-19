using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("User/{orderId}")]
        public async Task<IActionResult> GetUserByOrder(int orderId)
        {
            return Ok(await _service.GetUserByOrder(orderId));
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(OrderStatusEnum status)
        {
            return Ok(await _service.GetByStatusAsync(status));
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return Ok(await _service.GetByUserIdAsync(userId));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            return Ok(await _service.AddAsync(order));
        }

        [HttpPatch("change-status/{orderId}")]
        public async Task<IActionResult> ChangeStatus(int orderId, OrderStatusEnum status)
        {
            return Ok(await _service.ChangeStatusAsync(orderId, status));
        }
    }
}
