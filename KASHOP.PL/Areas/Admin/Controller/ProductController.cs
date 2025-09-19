using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult GetALL() => Ok(_service.GetAllProduct(Request));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var result = await _service.CreateWithFile(request);
            if (result > 0)
            {
                return Ok(new { Message = "Product created successfully", ProductId = result });
            }
            return BadRequest(new { Message = "Failed to create product" });
        }
    }
}
