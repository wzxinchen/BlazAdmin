using BlazAdmin.Abstract;
using Blazui.Component;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BAdminPageBase : BComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        /// <summary>
        /// 当前页面允许访问的角色
        /// </summary>
        protected string Roles { get; private set; }

        public string Username { get; private set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var routes = GetType().GetCustomAttributes(false)
                .OfType<RouteAttribute>()
                .Select(x => x.Template)
                .ToArray();
            Roles = await UserService.GetRolesAsync(routes);
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                Roles += ",管理员";
            }
            else
            {
                Roles = "管理员";
            }
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState?.User;
            Username = user?.Identity?.Name;
        }
    }
}
