using Blazui.Component;
using Blazui.Component.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BRoleManagementBase : BAdminPageBase
    {
        protected List<RoleModel> Roles { get; private set; } = new List<RoleModel>();
        protected BTable table;


        public async Task CreateRoleAsync()
        {
            await DialogService.ShowDialogAsync<BRoleEdit>("创建角色", 400, new Dictionary<string, object>());
            await RefreshRolesAsync();
        }

        private async Task RefreshRolesAsync()
        {
            Roles = await UserService.GetRolesAsync();
            table.MarkAsRequireRender();
            RequireRender = true;
            StateHasChanged();
        }

        public async Task EditAsync(object role)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add(nameof(BRoleEdit.Role), role);
            await DialogService.ShowDialogAsync<BRoleEdit>("编辑角色", 400, parameters);
            await RefreshRolesAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (!firstRender)
            {
                return;
            }
            await RefreshRolesAsync();
        }

        public async Task Del(object role)
        {
            var confirm = await ConfirmAsync("将删除该角色下所有用户，确认删除该角色？");
            if (confirm != MessageBoxResult.Ok)
            {
                return;
            }
            var result = await UserService.DeleteRolesAsync(((RoleModel)role).Id);
            if (string.IsNullOrWhiteSpace(result))
            {
                await RefreshRolesAsync();
                return;
            }
            Toast(result);
        }
    }
}
