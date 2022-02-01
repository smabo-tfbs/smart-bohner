using Microsoft.Extensions.DependencyInjection;
using SmartBohner.Gpio.Abstractions;
using SmartBohner.Gpio.Debugging;

namespace SmartBohner.Gpio.Extensions
{
    public static class GpioExtension
    {
        public static void InitGpio(this IServiceCollection serviceCollection)
        {
#if DEBUG
            serviceCollection.AddScoped<IPinService, DebugPinService>();
            serviceCollection.AddSingleton<IPinServiceFactory, DebugPinServiceFactory>();
            serviceCollection.AddSingleton<IGpioChangeContainer, DebugGpioChangeController>();
#else
            serviceCollection.AddTransient<IPinService, PinService>();
            serviceCollection.AddSingleton<IPinServiceFactory, PinServiceFactory>();
            serviceCollection.AddSingleton<IGpioChangeContainer, GpioChangeContainer>();
#endif
        }
    }
}
