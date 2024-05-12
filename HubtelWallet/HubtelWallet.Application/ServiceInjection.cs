using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HubtelWallet.Application
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
