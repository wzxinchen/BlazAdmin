using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin.Core.Abstract
{
    public interface IUserService
    {
        Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword);
        Task<string> CreateUserAsync(string username, string password);
        Task<string> CreateRoleAsync(string roleName, string id);
        Task<List<object>> GetUsersAsync();
        Task<string> AddToRoleAsync(string username, params string[] roles);
        Task<string> CheckPasswordAsync(string username, string password);
    }
}
