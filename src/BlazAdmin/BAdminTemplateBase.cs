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

        protected string defaultMenuIndex;
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
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
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

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            var path = new Uri(NavigationManager.Uri).LocalPath;
            AddTab(path);
        }

        private void AddTab(string path)
        {
            var type = routeService.GetComponent(path);
            var model = (MenuModel)CurrentMenu.Model;
            ActiveTabName = model.Name ?? model.Route;
            if (Tabs.Any(x => x.Name == ActiveTabName))
            {
                StateHasChanged();
                return;
            }
            Tabs.Add(new TabModel()
            {
                Title = model.Title ?? model.Label,
                Name = ActiveTabName,
                Content = type
            });
        }
    }
}
