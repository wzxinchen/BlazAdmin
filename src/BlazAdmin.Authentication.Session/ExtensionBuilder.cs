using BlazAdmin.Core;
using BlazAdmin.Core.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Authentication.Session
{
    public static class ExtensionBuilder
    {
        public static IServiceCollection AddBlazAdminServices<TUser, TUserService>(this IServiceCollection services)
            where TUserService : class, IUserService
        {
            return services.AddBlazAdminServices<TUser, TUserService>(null);
        }
        public static IServiceCollection AddBlazAdminServices<TUser, TUserService>(this IServiceCollection services, Action<SessionOptions> options)
            where TUserService : class, IUserService
        {
            services.AddScoped<IUserService, TUserService>();
            services.AddBlazAdminCoreServices();
            if (options == null)
            {
                services.AddSession();
            }
            else
            {
                services.AddSession(options);
            }
            return services;
        }

        public static IApplicationBuilder UseBlazAdmin(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSession();
            return applicationBuilder;
        }
        public static IApplicationBuilder UseBlazAdmin(this IApplicationBuilder applicationBuilder, SessionOptions options)
        {
            applicationBuilder.UseSession(options);
            return applicationBuilder;
        }
    }
}
