using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Gpio;
using SmartBohner.Gpio.Abstractions;
using SmartBohner.Gpio.Extensions;
using System.Device.I2c;

namespace SmartBohner.ControlUnit.Extensions
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {
            serviceCollection.InitGpio();

            serviceCollection.AddTransient<IPinSwitcher, PinSwitcher>();
            serviceCollection.AddTransient<ICoffeeMachineService, CoffeeMachineService>();
            serviceCollection.AddTransient<ICoffeeService, CoffeeService>();
            serviceCollection.AddSingleton<IMaintenanceMessagingService, MaintenanceMessagingService>();
            serviceCollection.AddTransient<IMaintenanceService, MaintenanceService>();

            return serviceCollection;
        }
    }
}
