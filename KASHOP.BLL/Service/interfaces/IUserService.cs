using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service.interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetUserById(string id);
    Task<bool> BlockUserAsync(string userId, int days);
    Task<bool> IsBlockedAsync(string userId);
    Task<bool> UnBlockUserAsync(string userId);
    Task<bool> ChangeRoleUserAsync(string userId, string roleName);
}