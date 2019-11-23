using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BAdminTemplateBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string DefaultRoute { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
            if (new Uri(NavigationManager.Uri).LocalPath == "/" && !string.IsNullOrWhiteSpace(DefaultRoute))
            {
                NavigationManager.NavigateTo(DefaultRoute);
            }
        }
    }
}
