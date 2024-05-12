using FluentValidation;
using FluentValidation.AspNetCore;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using HubtelWallet.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Reflection.Metadata;

namespace HubtelWallet.Application
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.IncludeValidators();
            services.AddTransient<IServiceManager, ServiceManager>();
            return services;
        }

        public static IServiceCollection IncludeValidators(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }


    }
}
