using Microsoft.Extensions.DependencyInjection;
using SmartBohner.Web.Shared.SignalR;

namespace SmartBohner.Web.Shared.Extensions
{
    public static class SharedExtension
    {
        public static IServiceCollection RegisterShared(this IServiceCollection services)
        {
            services.AddSingleton<IWarningHubConnectionHandler, WarningHubConnectionHandler>();

            return services;
        }
    }
}
