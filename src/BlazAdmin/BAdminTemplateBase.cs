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
            if (new Uri(NavigationManager.Uri).LocalPath == "/" && !string.IsNullOrWhiteSpace(DefaultRoute))
            {
                NavigationManager.NavigateTo(DefaultRoute);
                return;
            }
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            var path = new Uri(NavigationManager.Uri).LocalPath;
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
            StateHasChanged();
        }
    }
}
