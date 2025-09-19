using KASHOP.BLL.Service.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _service.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute]string id)
        {
            return Ok(await _service.GetUserById(id));
        }
        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute]string userId, [FromBody]int days)
        {
            return Ok(await _service.BlockUserAsync(userId, days));
        }
        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute]string userId)
        {
            return Ok(await _service.UnBlockUserAsync(userId));
        }
        [HttpPatch("isblocked/{userId}")]
        public async Task<IActionResult> IsBlocked([FromRoute]string userId)
        {
            return Ok(await _service.IsBlockedAsync(userId));
        }
        
        [HttpPatch("changeRole/{userId}")]
        public async Task<IActionResult> ChangeRole([FromRoute]string userId, [FromBody]string roleName)
        {
            return Ok(await _service.ChangeRoleUserAsync(userId, roleName));
        }
    }
}
