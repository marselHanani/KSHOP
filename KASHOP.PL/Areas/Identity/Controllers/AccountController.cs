using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Area.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AccountController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> registerAsync([FromBody] RegisterRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var response = await _service.RegisterAsync(request, Request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> loginAsync([FromBody] LoginRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var response = await _service.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            try
            {
                var response = await _service.ConfirmEmail(token,userId);
                return Ok(response);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var response = await _service.ForgetPasswordAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }
            try
            {
                var response = await _service.ResetPasswordAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
