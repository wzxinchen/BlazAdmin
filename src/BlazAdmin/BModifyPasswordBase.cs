using Blazui.Component;
using Blazui.Component.Form;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BModifyPasswordBase : BComponentBase
    {
        protected BForm form;
        internal void Modify()
        {
            if (!form.IsValid())
            {
                return;
            }

            var info = form.GetValue<ModifyPasswordModel>();
            _ = DialogService.CloseDialogAsync(this, info);
        }
    }
}
