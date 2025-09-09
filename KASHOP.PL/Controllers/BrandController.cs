using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService service) : ControllerBase
    {
        private readonly IBrandService _service = service;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = _service.GetById(id);
            if (brand == null)
            {
                return NotFound($"Brand with ID {id} not found.");
            }
            return Ok(brand);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] BrandRequest request)
        {
            var result = await _service.CreateWithFile(request);
            if (result > 0)
            {
                return Ok(new { Message = "Product created successfully", BrandId = result });
            }
            return BadRequest(new { Message = "Failed to create product" });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }
            var result = _service.Update(id, request);
            if (result == 0)
            {
                return NotFound($"Brand with ID {id} not found.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            if (result == 0)
            {
                return NotFound($"Brand with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
