using Blazui.Component;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BAdminPageBase : BComponentBase
    {
        [Inject]
        public SignInManager<IdentityUser> SignInManager { get; set; }

        public IdentityUser User { get; private set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var username = await GetUsernameAsync();
            User = await SignInManager.UserManager.FindByNameAsync(username);
        }
        public async System.Threading.Tasks.Task<string> GetUsernameAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity.Name;
        }
    }
}
