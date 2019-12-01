using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazAdmin
{
    public class BCreateSuperUserBase : BComponentBase
    {
        internal BForm form;
        [Parameter]
        public LoginInfoModel DefaultUser { get; set; }

        [Inject]
        UserManager<IdentityUser> UserManager { get; set; }

        [Inject]
        RoleManager<IdentityRole> RoleManager { get; set; }
        [Inject]
        SignInManager<IdentityUser> SignInManager { get; set; }
        [CascadingParameter]
        public BAdminTemplateBase AdminTemplate { get; set; }
        protected InputType passwordType = InputType.Password;
        internal void TogglePassword()
        {
            if (passwordType == InputType.Password)
            {
                passwordType = InputType.Text;
            }
            else
            {
                passwordType = InputType.Password;
            }
        }

        internal async System.Threading.Tasks.Task CreateAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            var model = form.GetValue<LoginInfoModel>();
            var identityUser = new IdentityUser(model.Username);
            var createResult = await UserManager.CreateAsync(identityUser, model.Password);
            if (!createResult.Succeeded)
            {
                await AlertAsync(createResult.Errors.First().Description);
                return;
            }
            var identityRole = new IdentityRole("管理员");
            identityRole.Id = "admin";
            var createRoleResult = await RoleManager.CreateAsync(identityRole);
            if (!createRoleResult.Succeeded)
            {
                await UserManager.DeleteAsync(identityUser);
                await AlertAsync(createRoleResult.Errors.First().Description);
                return;
            }
            var addResult = await UserManager.AddToRoleAsync(identityUser, "管理员");
            if (!addResult.Succeeded)
            {
                await UserManager.DeleteAsync(identityUser);
                await RoleManager.DeleteAsync(identityRole);
                await AlertAsync(addResult.Errors.First().Description);
                return;
            }
            var result = await SignInManager.CheckPasswordSignInAsync(identityUser, model.Password, false);
            if (result.Succeeded)
            {
                await form.SubmitAsync("/account/login");
                return;
            }
        }
    }
}
