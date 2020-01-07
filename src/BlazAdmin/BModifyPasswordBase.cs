using Blazui.Component;
using Blazui.Component.Form;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BModifyPasswordBase : BAdminPageBase
    {
        protected BForm form;

        [Parameter]
        public DialogOption Dialog { get; set; }

        public virtual async System.Threading.Tasks.Task ModifyAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            var info = form.GetValue<ModifyPasswordModel>();

            var result = await UserService.ChangePasswordAsync(Username, info.OldPassword, info.NewPassword);
            if (string.IsNullOrWhiteSpace(result))
            {
                _ = Dialog.CloseDialogAsync(info);
                return;
            }
            Toast(result);
        }
    }
}
