using Blazui.Component;
using Blazui.Component.EventArgs;
using Blazui.Component.Form;
using Blazui.Component.NavMenu;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin
{
    public class BAdminTemplateBase : BComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private RouteService routeService { get; set; }

        protected BForm form;
        [Inject]
        private MessageService MessageService { get; set; }
        [Inject]
        SignInManager<IdentityUser> SignInManager { get; set; }

        [Inject]
        private MessageBox MessageBox { get; set; }

        protected string defaultMenuIndex;

        [Parameter]
        public LoginInfoModel DefaultUser { get; set; }

        protected string username;
        [Parameter]
        public RenderFragment LoginPage { get; set; }
        [Parameter]
        public RenderFragment CreatePage { get; set; }
        [Parameter]
        public float NavigationWidth { get; set; } = 250;
        /// <summary>
        /// 导航菜单栏标题
        /// </summary>
        [Parameter]
        public string NavigationTitle { get; set; } = "BlazAdmin 后台模板";

        [Parameter]
        public List<MenuModel> Menus { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string DefaultRoute { get; set; }

        internal string ActiveTabName { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        protected IMenuItem CurrentMenu { get; set; }

        /// <summary>
        /// 页面刚刚加载完成时自动加载选项卡的动作是否完成
        /// </summary>
        private bool isLoadRendered = false;

        internal async Task ModifyPasswordAsync()
        {
            var result = await DialogService.ShowDialogAsync<BModifyPassword, ModifyPasswordModel>("修改密码", 500);
            Alert(result.Result.NewPassword);
        }

        internal async System.Threading.Tasks.Task LogoutAsync()
        {
            var result = await MessageBox.ConfirmAsync("是否确认注销登录？");
            if (result != MessageBoxResult.Ok)
            {
                return;
            }

            await form.SubmitAsync("/account/logout");
        }
        /// <summary>
        /// 初始 Tab 集合
        /// </summary>
        [Parameter]
        public ObservableCollection<TabModel> Tabs { get; set; } = new ObservableCollection<TabModel>();
        [Inject]
        NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var path = new Uri(NavigationManager.Uri).LocalPath;
            if (path == "/" && !string.IsNullOrWhiteSpace(DefaultRoute))
            {
                NavigationManager.NavigateTo(DefaultRoute);
                return;
            }

            defaultMenuIndex = path;
            FixMenuInfo(Menus);
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            username = user.Identity.Name;
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            var path = new Uri(e.Location).LocalPath;
            AddTab(path);
        }

        void FixMenuInfo(List<MenuModel> menus)
        {
            foreach (var menu in menus)
            {
                menu.Name = menu.Name ?? menu.Route;
                menu.Title = menu.Title ?? menu.Label;
                FixMenuInfo(menu.Children);
            }
        }

        internal void Refresh()
        {
            StateHasChanged();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (!isLoadRendered)
            {
                isLoadRendered = true;
                AddTab(defaultMenuIndex);
            }
        }

        protected void OnRouteChanging(BChangeEventArgs<string> arg)
        {
            //arg.DisallowChange = true;
            //AddTab(arg.NewValue);
        }

        private void AddTab(string path)
        {
            var type = routeService.GetComponent(path);
            if (type == null)
            {
                if (path != "/")
                {
                    MessageService.Show($"路由为 {path} 的页面未找到", MessageType.Warning);
                }
                return;
            }
            ActiveTabName = path;
            if (!Tabs.Any(x => x.Name == ActiveTabName))
            {
                if (CurrentMenu == null)
                {
                    return;
                }
                var model = (MenuModel)CurrentMenu.Model;
                Tabs.Add(new TabModel()
                {
                    Title = model.Title ?? model.Label,
                    Name = ActiveTabName,
                    Content = type
                });
            }
            StateHasChanged();
        }
    }
}
