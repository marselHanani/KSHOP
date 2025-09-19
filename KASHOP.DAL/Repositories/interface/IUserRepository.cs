using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositories.Interface;

public interface IUserRepository
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(string id);
    Task<bool> BlockUserAsync(string userId, int days);
    Task<bool> IsBlockedAsync(string userId);
    Task<bool> UnBlockUserAsync(string userId);
    Task<bool> ChangeRoleUserAsync(string userId, string roleName);
    
}