using System.Reflection;
using Agenda.API.Extensions;
using Agenda.Application.Interfaces;
using Agenda.Application.Services;
using Agenda.Application.Services.Admin;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure;
using Agenda.Infrastructure.Repositories;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.API.Configurations
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();

            //repos
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IInteractionRepository, InteractionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            //AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
            },
               Assembly.GetAssembly(typeof(PhonebookService)));

            //Services
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IPhonebookService, PhonebookService>();
            services.AddScoped<IContactManagementService, ContactManagementService>();
            services.AddScoped<IInteractionService, InteractionService>();

            return services;
        }

    }
}
