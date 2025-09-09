using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        private readonly ICategoryService _service = service;


        [HttpGet("")]
        public IActionResult getAllCategories()
        {
           return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult getCategory([FromRoute]int id)
        {
            var category = _service.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("")]
        public IActionResult createCategory([FromBody] CategoryRequest request)
        {
            var id = _service.Add(request);
            return CreatedAtAction(nameof(getCategory), new {id},null);
        }

        [HttpPatch("{id}")]
        public IActionResult updateCategory([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var update = _service.Update(id, request);
            return update > 0 ? Ok() : NotFound();
        }
        [HttpPut("{id}/toggle-status")]
        public IActionResult toggleStatus([FromRoute] int id)
        {
            var result = _service.ToggleStatus(id);
            return result ? Ok(new {message = "Status Toggled"}) : NotFound();
        }

            [HttpDelete("{id}")]
        public IActionResult deleteCategory([FromRoute]int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
