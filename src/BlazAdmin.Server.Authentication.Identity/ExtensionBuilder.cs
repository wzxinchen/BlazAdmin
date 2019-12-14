using BlazAdmin.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Authentication.Identity
{
    public static class ExtensionBuilder
    {
        public static IServiceCollection AddBlazAdminServer<TDbContext>(this IServiceCollection services)
            where TDbContext : IdentityDbContext
        {
            return services.AddBlazAdminServer<TDbContext>("BlazAdmin 接口服务");
        }

        public static IServiceCollection AddBlazAdminServer<TDbContext>(this IServiceCollection services, string swaggerTitle)
            where TDbContext : IdentityDbContext
        {
            services.AddControllers()
                .AddApplicationPart(typeof(ExtensionBuilder).Assembly);
            services.AddBlazAdminServer<IdentityUser, UserService, TDbContext>(null);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1",new Swashbuckle.AspNetCore.Swagger.Info
            //    {
            //        Title = swaggerTitle,
            //        Version = "v1"
            //    });
            //    c.AddSecurityDefinition("Cookie", new ApiKeyScheme { In = "header", Description = "Please enter Cookie with Bearer into field", Name = "Authorization", Type = "apiKey" });
            //    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            //    {
            //        { "Bearer", Enumerable.Empty<string>() },
            //    });
            //});
            return services;
        }
        //public static IServiceCollection AddBlazAdminServices<TUser, TUserService, TDbContext>(this IServiceCollection services)
        //    where TUser : IdentityUser
        //    where TDbContext : IdentityDbContext
        //    where TUserService : class, IUserService
        //{
        //    return services.AddBlazAdminServices<TUser, TUserService, TDbContext>(null);
        //}

        //public static IServiceCollection AddBlazAdminServices<TDbContext>(this IServiceCollection services)
        //    where TDbContext : IdentityDbContext
        //{
        //    return services.AddBlazAdminServices<IdentityUser, UserService, TDbContext>(null);
        //}

        private static IServiceCollection AddBlazAdminServer<TUser, TUserService, TDbContext>(this IServiceCollection services, Action<IdentityOptions> optionConfigure)
            where TUser : IdentityUser
            where TDbContext : IdentityDbContext
            where TUserService : class, IUserService
        {
            services.AddScoped<IUserService, TUserService>();
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
            var builder = services.AddIdentityCore<TUser>(o =>
              {
                  o.Stores.MaxLengthForKeys = 128;
              }).AddRoles<IdentityRole>()
              .AddSignInManager()
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<TDbContext>();

            if (optionConfigure != null)
            {
                services.Configure(optionConfigure);
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
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            return services;
        }
        public static IApplicationBuilder UseBlazAdminServer(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseBlazAdminServerCore();
            //applicationBuilder.UseSwagger();
            //applicationBuilder.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazAdmin Api V1");
            //    c.RoutePrefix = string.Empty;
            //});
            return applicationBuilder;
        }
    }
}
