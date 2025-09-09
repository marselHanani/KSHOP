using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.PL.Areas.Customer.Controller
{
    [Route("api/[area]/[controller]")]
    [Area("Customer")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _service;

        public CheckOutController(ICheckOutService service)
        {
            _service = service;
        }

        [HttpPost("payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] CheckOutRequest request)
        {
            var userId = User.FindFirstValue("Id");
            if (userId == null)
            {
                return Unauthorized();
            }
            var response = await _service.ProcessPaymentAsync(request, userId, Request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("Success/{orderId}")]
        public async Task<IActionResult> Success(int orderId)
        {
            var result = await _service.HandlePaymentSuccessAsync(orderId);
            if (result)
            {
                return Ok("Payment Successful");
            }
            return BadRequest("Payment Failed");
        }
    }
}
