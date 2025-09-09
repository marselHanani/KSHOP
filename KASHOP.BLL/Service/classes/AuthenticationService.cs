using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Http;
namespace KASHOP.BLL.Service.classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userRepo;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signIn;

        public AuthenticationService(UserManager<ApplicationUser> userRepo
            , IConfiguration configuration,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signIn)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _emailSender = emailSender;
            _signIn = signIn;
        }
        public async Task<UserResponse> LoginAsync(DAL.DTO.Request.LoginRequest request)
        {
            var user = await _userRepo.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _signIn.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.Succeeded)
            {
                return new UserResponse()
                {
                    Token = await CreateTonkenAsync(user),
                };
            }else if(result.IsLockedOut)
            {
                throw new Exception("User is locked out");
            }
            else if (result.IsNotAllowed)
            {
                throw new Exception("Please confirm your email");
            }
            else
            {
                throw new Exception("Invalid email or password");
            }
        }

        public async Task<string> ConfirmEmail(string token , string userId)
        {
            var user = await _userRepo.FindByIdAsync(userId);
            if(user is null)
            {
                throw new Exception($"Unable to find user {userId}");
            }
            token = Uri.UnescapeDataString(token);
            var result = await _userRepo.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirm succefully";
            }
            return "Email confirmation faild";
        }
        public async Task<UserResponse> RegisterAsync(DAL.DTO.Request.RegisterRequest request,HttpRequest Request)
        {
            var user = new ApplicationUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber

            };
            var result = await _userRepo.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                var token = await _userRepo.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken= Uri.EscapeDataString(token);
                var emailUrl = $"{Request.Scheme}:{Request.Host}/api/identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "Confirm Email", "<h1>Pleaes click to the link to confirm your Eamil<h1>"+
                    $"<a href={emailUrl}>confirm<a>");

                await _userRepo.AddToRoleAsync(user, "Customer");
                return new UserResponse()
                { 
                    Token = "Please confirm your email to get the token"
                };
            }
            else
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<string> CreateTonkenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>
{
            new Claim("Email", user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim("PhoneNumber", user.PhoneNumber ?? ""),
            new Claim("Id", user.Id ?? "")
            };

            var Roles = await _userRepo.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim("role", role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["secretKey"]));
            var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> ForgetPasswordAsync(ForgetPasswordRequest request)
        {
            var user = await _userRepo.FindByEmailAsync(request.Email);
            if(user is null)
            {
                throw new Exception("User not found");
            }
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.ExpireCodeResetPassword = DateTime.UtcNow.AddMinutes(15);
            await _userRepo.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email,"Reset Password", $"<h1>Your reset code is : {code}<h1>");
            return "Please check your email to reset password";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepo.FindByEmailAsync(request.Email);
            if(user is null)
            {
                throw new Exception("User not found");
            }
            if(user.CodeResetPassword != request.ResetCode)
            {
                throw new Exception("Invalid code");
            }
            if(user.ExpireCodeResetPassword < DateTime.UtcNow)
            {
                throw new Exception("Code expired");
            }
            var token = await _userRepo.GeneratePasswordResetTokenAsync(user);
            var result = await _userRepo.ResetPasswordAsync(user, token, request.NewPassword);
            if(result.Succeeded)
            {
                user.CodeResetPassword = null;
                user.ExpireCodeResetPassword = null;
                await _userRepo.UpdateAsync(user);
                return "Password reset successfully";
            }
            else
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
