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
        Task<string> DeleteUserAsync(object user);

        /// <summary>
        /// 仅检查密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> CheckPasswordAsync(string username, string password);
        Task<string> CreateSuperUserAsync(string username, string password);
        Task<string> LogoutAsync();

        /// <summary>
        /// 检查密码，同时设置登录Cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> LoginAsync(string username, string password);
    }
}
