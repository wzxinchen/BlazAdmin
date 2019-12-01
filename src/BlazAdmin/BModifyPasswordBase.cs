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


        public virtual async System.Threading.Tasks.Task ModifyAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            var info = form.GetValue<ModifyPasswordModel>();

            var result = await SignInManager.UserManager.ChangePasswordAsync(User, info.OldPassword, info.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    if (item.Code == "PasswordMismatch")
                    {
                        Toast("旧密码错误");
                        return;
                    }
                    Toast(item.Description);
                    return;
                }
                return;
            }
            _ = DialogService.CloseDialogAsync(this, info);
        }
    }
}
