using BlazAdmin.Client;
using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Client
{
    public class BLoginBase : BAdminPageBase
    {
        public BForm Form { get; internal set; }
        [Parameter]
        public LoginInfoModel DefaultUser { get; set; }

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
            var result = await UserService.CheckPasswordAsync(model.Username, model.Password);
            if (string.IsNullOrWhiteSpace(result))
            {
                await Form.SubmitAsync(UserService.Options.ServerUrl + UserService.Options.LoginUrl + "?callback=" + NavigationManager.Uri);
                return;
            }
            Toast(result);
        }
    }
}
