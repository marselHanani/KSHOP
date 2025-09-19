using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.DAL.Repositories.classes;

public class UserRepository: IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var dto = user.Adapt<UserDto>();
            var roles = await _userManager.GetRolesAsync(user);
            dto.RoleName = roles.FirstOrDefault();
            userDtos.Add(dto);
        }
        return userDtos;
    }

    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id); 
        return user.Adapt<UserDto>();
    }

    public async Task<bool> BlockUserAsync(string userId, int days)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        user.LockoutEnd = DateTime.UtcNow.AddDays(days);
        await _userManager.UpdateAsync(user);
        return true;
    }
    public async Task<bool> UnBlockUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        user.LockoutEnd = null;
        await _userManager.UpdateAsync(user);
        return true;
    }

    public async Task<bool> IsBlockedAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        return user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow;
    }

    public async Task<bool> ChangeRoleUserAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return false;
        var role = await _userManager.GetRolesAsync(user);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, role);
        var addResult = await _userManager.AddToRoleAsync(user, roleName);
        return removeResult.Succeeded && addResult.Succeeded;
    }
}