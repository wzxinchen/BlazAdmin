using Blazui.Component;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Client
{
    public static class ExtensionBuilder
    {
        public static IServiceCollection AddBlazAdmin(this IServiceCollection services)
        {
            return services.AddBlazAdmin(null);
        }
        public static IServiceCollection AddBlazAdmin(this IServiceCollection services, Action<ServerOptions> optionsSetup)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddBlazuiServices();
            services.AddSingleton<RouteService>();
            if (optionsSetup != null)
            {
                services.Configure(optionsSetup);
            }
            else
            {
                services.Configure<ServerOptions>(options =>
                {

                });
            }
            services.AddScoped<UserService>();
            return services;
        }
    }
}
