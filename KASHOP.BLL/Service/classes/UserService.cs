using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KASHOP.BLL.Service.classes;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<UserDto> GetUserById(string id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<bool> BlockUserAsync(string userId, int days)
    {
        return await _repo.BlockUserAsync(userId, days);
    }

    public async Task<bool> IsBlockedAsync(string userId)
    {
       return await _repo.IsBlockedAsync(userId);
    }

    public async Task<bool> UnBlockUserAsync(string userId)
    {
       return await _repo.UnBlockUserAsync(userId); 
    }

    public async Task<bool> ChangeRoleUserAsync(string userId, string roleName)
    {
        return await _repo.ChangeRoleUserAsync(userId, roleName);
    }
}