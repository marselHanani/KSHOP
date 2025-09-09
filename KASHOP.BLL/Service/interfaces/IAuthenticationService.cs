using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(DAL.DTO.Request.LoginRequest request);
        Task<UserResponse> RegisterAsync(DAL.DTO.Request.RegisterRequest request , HttpRequest Request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<string> ForgetPasswordAsync(ForgetPasswordRequest request);
        Task<string> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
