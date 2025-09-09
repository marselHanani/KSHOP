using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public IActionResult Add([FromBody] BrandRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }
            var id = _service.Add(request);
            return CreatedAtAction(nameof(GetById), new { id }, request);
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

