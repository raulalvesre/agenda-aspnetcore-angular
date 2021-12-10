using System;
using Agenda.MVC.ApiHttpServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.LogoutPath = new PathString("/logout");
                    options.AccessDeniedPath = new PathString("/login");
                    options.SlidingExpiration = false;
                });

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<LoginService>();
            services.AddScoped<PhonebookService>();
            services.AddScoped<UserManagementService>();
            services.AddScoped<ContactManagementService>();
            services.AddScoped<InteractionsService>();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            return services;
        }

    }
}
