using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Blazui.Component;
using Blazui.Component.Table;

namespace BlazAdmin
{
    public class BUserManagementBase : BAdminPageBase
    {
        protected List<UserModel> Users { get; private set; } = new List<UserModel>();
        protected BTable table;


        public async Task CreateUserAsync()
        {
            await DialogService.ShowDialogAsync<BUserEdit>("创建用户", 400, new Dictionary<string, object>());
            await RefreshUsersAsync();
        }

        private async Task RefreshUsersAsync()
        {
            Users = await UserService.GetUsersAsync();
            table.MarkAsRequireRender();
            RequireRender = true;
            StateHasChanged();
        }

        public async Task EditAsync(object user)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add(nameof(BUserEdit.User), user);
            await DialogService.ShowDialogAsync<BUserEdit>("编辑用户", 400, parameters);
            await RefreshUsersAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (!firstRender)
            {
                return;
            }
            Users = await UserService.GetUsersAsync();
            table.MarkAsRequireRender();
            RequireRender = true;
            StateHasChanged();
        }

        public async Task Del(object user)
        {
            var confirm = await ConfirmAsync("确认删除该用户？");
            if (confirm != MessageBoxResult.Ok)
            {
                return;
            }
            var result = await UserService.DeleteUsersAsync(((UserModel)user).Id);
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }
            await RefreshUsersAsync();
        }

        public async Task Reset(object user)
        {
            var confirm = await ConfirmAsync("确认将该用户的密码重置为 12345678 吗？");
            if (confirm != MessageBoxResult.Ok)
            {
                return;
            }
            var error = await UserService.ResetPasswordAsync(((UserModel)user).Id, "12345678");
            if (string.IsNullOrWhiteSpace(error))
            {
                return;
            }
            Toast(error);
        }
    }
}
