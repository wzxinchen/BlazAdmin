using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin.Authentication.Identity
{
    public class UserService : UserServiceBase<IdentityUser, IdentityRole>
    {
        public UserService(SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) : base(signInManager, roleManager)
        {
        }

        public override async Task<string> CreateRoleAsync(string roleName, string id)
        {
            var role = new IdentityRole(roleName);
            role.Id = id;
            var result = await RoleManager.CreateAsync(role);
            return GetResultMessage(result);
        }

        public override async Task<string> CreateUserAsync(string username, string password)
        {
            var user = new IdentityUser(username);
            var result = await SignInManager.UserManager.CreateAsync(user, password);
            return GetResultMessage(result);
        }

        public override async Task<string> DeleteUserAsync(object user)
        {
            var result = await SignInManager.UserManager.DeleteAsync((IdentityUser)user);
            return GetResultMessage(result);
        }
    }
}
