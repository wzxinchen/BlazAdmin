using BlazAdmin.Core;
using BlazAdmin.Core.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Authentication.Identity
{
    public static class ExtensionBuilder
    {
        public static IServiceCollection AddBlazAdminServices<TUser, TUserService, TDbContext>(this IServiceCollection services)
            where TUser : IdentityUser
            where TDbContext : IdentityDbContext
        {
            return services.AddBlazAdminServices<TUser, TUserService, TDbContext>(null);
        }
        public static IServiceCollection AddBlazAdminServices<TDbContext>(this IServiceCollection services)
            where TDbContext : IdentityDbContext
        {
            return services.AddBlazAdminServices<IdentityUser, UserService, TDbContext>(null);
        }
        public static IServiceCollection AddBlazAdminServices<TUser, TUserService, TDbContext>(this IServiceCollection services, Action<IdentityOptions> optionConfigure)
            where TUser : IdentityUser
            where TDbContext : IdentityDbContext
        {
            services.AddScoped<IUserService, UserService>();
            services.AddBlazAdminCoreServices();
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
            services.AddIdentityCore<TUser>(o =>
            {
                o.Stores.MaxLengthForKeys = 128;
            }).AddRoles<IdentityRole>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<TDbContext>();
            if (optionConfigure != null)
            {
                services.Configure<IdentityOptions>(optionConfigure);
            }
            else
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                });
            }
            return services;
        }
    }
}
