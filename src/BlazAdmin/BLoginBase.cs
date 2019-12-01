using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BLoginBase : BComponentBase
    {
        internal BForm form;
        [Parameter]
        public LoginInfoModel DefaultUser { get; set; }

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

        internal async System.Threading.Tasks.Task LoginAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            var model = form.GetValue<LoginInfoModel>();
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                await form.SubmitAsync("/account/login");
                return;
            }
            Toast("用户名或密码错误，登录失败");
        }
    }
}
