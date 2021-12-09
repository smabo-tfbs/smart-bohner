using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit.Extensions
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICoffeeMachineService, CoffeeMachineService>();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();
            serviceCollection.AddTransient<IDebugPinService, DebugPinService>();
            serviceCollection.AddSingleton<IMaintenanceMessagingService, MaintenanceMessagingService>();
            serviceCollection.AddTransient<IMaintenanceService, MaintenanceService>();

            return serviceCollection;
        }
    }
}
