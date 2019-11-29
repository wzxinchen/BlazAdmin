using Blazui.Component;
using Blazui.Component.EventArgs;
using Blazui.Component.NavMenu;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BlazAdmin
{
    public class BAdminTemplateBase : ComponentBase
    {
        [Inject]
        private RouteService routeService { get; set; }

        [Inject]
        private MessageService MessageService { get; set; }

        protected string defaultMenuIndex;

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

        /// <summary>
        /// 初始 Tab 集合
        /// </summary>
        [Parameter]
        public ObservableCollection<TabModel> Tabs { get; set; } = new ObservableCollection<TabModel>();
        [Inject]
        NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
            var path = new Uri(NavigationManager.Uri).LocalPath;
            if (path == "/" && !string.IsNullOrWhiteSpace(DefaultRoute))
            {
                NavigationManager.NavigateTo(DefaultRoute);
                return;
            }

            defaultMenuIndex = path;
            FixMenuInfo(Menus);
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
            arg.DisallowChange = true;
            AddTab(arg.NewValue);
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
