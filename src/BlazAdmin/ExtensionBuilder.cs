using Blazui.Component;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public static class ExtensionBuilder
    {
        public static IServiceCollection AddBlazAdminServices(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddBlazuiServices();
            services.AddSingleton<RouteService>();
            return services;
        }
    }
}
