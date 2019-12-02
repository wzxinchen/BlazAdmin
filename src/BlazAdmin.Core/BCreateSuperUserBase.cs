using BlazAdmin.Core;
using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BlazAdmin.Core
{
    public class BCreateSuperUserBase : BAdminPageBase
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
        public BAdminBase AdminTemplate { get; set; }
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
            string err;
            using (var scope = new TransactionScope())
            {
                err = await UserService.CreateUserAsync(model.Username, model.Password);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Toast(err);
                    return;
                }
                err = await UserService.CreateRoleAsync("管理员", "admin");
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Toast(err);
                    return;
                }
                err = await UserService.AddToRoleAsync(model.Username, "管理员");
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Toast(err);
                    return;
                }
                scope.Complete();
            }
            err = await UserService.CheckPasswordAsync(model.Username, model.Password);
            if (string.IsNullOrWhiteSpace(err))
            {
                await form.SubmitAsync("/account/login?callback=" + NavigationManager.Uri);
                return;
            }
        }
    }
}
