using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;
using SmartBohner.ControlUnit.Gpio.Debugging;
using System.Device.I2c;

namespace SmartBohner.ControlUnit.Extensions
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPinSwitcher, PinSwitcher>();
            serviceCollection.AddTransient<ICoffeeMachineService, CoffeeMachineService>();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();

#if DEBUG
            serviceCollection.AddScoped<IPinService, DebugPinService>();
            serviceCollection.AddSingleton<IPinServiceFactory, DebugPinServiceFactory>();
            serviceCollection.AddSingleton<IGpioChangeContainer, DebugGpioChangeController>();
#else
            serviceCollection.AddSingleton<IPinServiceFactory, PinServiceFactory>();
            serviceCollection.AddSingleton<IGpioChangeContainer, GpioChangeContainer>();
#endif

            serviceCollection.AddSingleton<IMaintenanceMessagingService, MaintenanceMessagingService>();
            serviceCollection.AddTransient<IMaintenanceService, MaintenanceService>();

            return serviceCollection;
        }
    }
}
