using Blazui.Component;
using Blazui.Component.Form;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BRoleEditBase : BAdminPageBase
    {
        internal BForm form;
        [Parameter]
        public RoleModel Role { get; set; }

        [Parameter]
        public DialogOption Dialog { get; set; }
        private bool isCreate = false;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            isCreate = Role == null;
        }
        public async System.Threading.Tasks.Task SubmitAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            string error;
            Role = form.GetValue<RoleModel>();
            if (isCreate)
            {
                error = await UserService.CreateRoleAsync(Role.Name);
            }
            else
            {
                error = await UserService.UpdateRoleAsync(Role);
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                Toast(error);
                return;
            }
            _ = DialogService.CloseDialogAsync(this, (object)null);
        }
    }
}
