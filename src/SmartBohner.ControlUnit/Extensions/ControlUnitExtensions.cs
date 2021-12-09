using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;

namespace SmartBohner.ControlUnit.Extensions
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICoffeeMachineService, CoffeeMachineService>();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();
            serviceCollection.AddSingleton(PinServiceFactory.GetDebugPinService);
            serviceCollection.AddSingleton<IMaintenanceMessagingService, MaintenanceMessagingService>();
            serviceCollection.AddTransient<IMaintenanceService, MaintenanceService>();

            return serviceCollection;
        }
    }
}
