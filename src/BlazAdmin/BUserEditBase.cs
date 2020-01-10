using Blazui.Component;
using Blazui.Component.Form;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BUserEditBase : BAdminPageBase
    {
        internal BForm form;
        [Parameter]
        public UserModel EditingUser { get; set; }

        internal List<TransferItem> RoleItems;
        [Parameter]
        public DialogOption Dialog { get; set; }
        private bool isCreate = false;
        bool initilized = false;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            isCreate = User == null;
            if (initilized)
            {
                return;
            }
            initilized = true;
            RoleItems = UserService.GetRoles().Select(x => new TransferItem()
            {
                Id = x.Id,
                Label = x.Name
            }).ToList();
        }
        public async System.Threading.Tasks.Task SubmitAsync()
        {
            if (!form.IsValid())
            {
                return;
            }

            string error;
            EditingUser = form.GetValue<UserModel>();
            if (isCreate)
            {
                error = await UserService.CreateUserAsync(EditingUser.Username, EditingUser.Email, EditingUser.Password);
            }
            else
            {
                error = await UserService.UpdateUserAsync(EditingUser);
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
