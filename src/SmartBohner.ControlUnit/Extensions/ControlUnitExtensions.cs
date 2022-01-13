using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;
using System.Device.I2c;

namespace SmartBohner.ControlUnit.Extensions
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICoffeeMachineService, CoffeeMachineService>();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();
            serviceCollection.AddSingleton<PinServiceFactory>();

#if DEBUG
            serviceCollection.AddSingleton<IGpioChangeContainer, DebugGpioChangeController>();
#else
            serviceCollection.AddSingleton<IGpioChangeContainer, GpioChangeContainer>();
#endif

            serviceCollection.AddSingleton<IMaintenanceMessagingService, MaintenanceMessagingService>();
            serviceCollection.AddTransient<IMaintenanceService, MaintenanceService>();

            return serviceCollection;
        }
    }
}
