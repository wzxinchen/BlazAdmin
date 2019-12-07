using BlazAdmin.Core;
using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Core
{
    public class BLoginBase : BAdminPageBase
    {
        public BForm Form { get; internal set; }
        [Parameter]
        public LoginInfoModel DefaultUser { get; set; }

        [Inject]
        SignInManager<IdentityUser> SignInManager { get; set; }
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

        public virtual async System.Threading.Tasks.Task LoginAsync()
        {
            if (!Form.IsValid())
            {
                return;
            }

            var model = Form.GetValue<LoginInfoModel>();
            var err = await UserService.CheckPasswordAsync(model.Username, model.Password);
            if (string.IsNullOrWhiteSpace(err))
            {
                await Form.SubmitAsync("/account/login?callback=" + NavigationManager.Uri);
                return;
            }
            Toast("用户名或密码错误，登录失败");
        }
    }
}
