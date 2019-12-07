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

        public virtual async System.Threading.Tasks.Task CreateAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            var model = form.GetValue<LoginInfoModel>();
            var err = await UserService.CreateSuperUserAsync(model.Username, model.Password);
            if (string.IsNullOrWhiteSpace(err))
            {
                await form.SubmitAsync("/account/login?callback=" + NavigationManager.Uri);
                return;
            }
            Toast(err);
        }
    }
}
