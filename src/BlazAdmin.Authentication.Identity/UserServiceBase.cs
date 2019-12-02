using BlazAdmin.Core.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin.Authentication.Identity
{
    public abstract class UserServiceBase<TUser, TRole> : IUserService
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        protected readonly SignInManager<TUser> SignInManager;
        protected readonly RoleManager<TRole> RoleManager;

        protected string GetResultMessage(IdentityResult identity)
        {
            if (identity.Succeeded)
            {
                return string.Empty;
            }
            foreach (var item in identity.Errors)
            {
                return item.Description;
            }
            return string.Empty;
        }
        public UserServiceBase(SignInManager<TUser> signInManager, RoleManager<TRole> roleManager)
        {
            this.SignInManager = signInManager;
            this.RoleManager = roleManager;
        }

        public async Task<TUser> FindUserByNameAsync(string username)
        {
            return await SignInManager.UserManager.FindByNameAsync(username);
        }

        public async Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var user = await FindUserByNameAsync(username);
            if (user == null)
            {
                return "当前用户名不存在";
            }
            var result = await SignInManager.UserManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    if (item.Code == "PasswordMismatch")
                    {
                        return "旧密码错误";
                    }
                    return item.Description;
                }
            }
            return null;
        }

        public abstract Task<string> CreateUserAsync(string username, string password);

        public abstract Task<string> CreateRoleAsync(string roleName, string id);

        public async Task<string> AddToRoleAsync(string username, params string[] roles)
        {
            var user = await FindUserByNameAsync(username);
            var result = await SignInManager.UserManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    return item.Description;
                }
            }
            return string.Empty;
        }

        public async Task<string> CheckPasswordAsync(string username, string password)
        {
            var user = await FindUserByNameAsync(username);
            var result = await SignInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                return "用户名或密码错误";
            }
            return string.Empty;
        }
        //{
        //    var createResult = await signInManager.UserManager.CreateAsync(new IdentityUser(username), password);
        //    if (!createResult.Succeeded)
        //    {
        //        foreach (var item in createResult.Errors)
        //        {
        //            return item.Description;
        //        }
        //        return string.Empty;
        //    }
        //    return string.Empty;
        //}
    }
}
