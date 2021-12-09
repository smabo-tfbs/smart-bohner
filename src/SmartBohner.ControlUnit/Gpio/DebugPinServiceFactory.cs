using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using System.Device.Gpio;

namespace SmartBohner.ControlUnit.Gpio
{
    public class DebugPinServiceFactory
    {
        private readonly ILogger<DebugPinServiceFactory> logger;
        private readonly GpioController controller = new();

        public DebugPinServiceFactory(ILogger<DebugPinServiceFactory> logger)
        {
            this.logger = logger;
        }

        public DebugPinServiceFactory WithPinOpened(int pin)
        {
            try
            {
                controller.OpenPin(pin);
                logger.LogInformation($"Opened pin: {pin}");
            }
            catch (Exception)
            {
                logger.LogWarning($"Error opening pin: {pin}");
            }

            return this;
        }

        public IDebugPinService Build()
        {
            return new DebugPinService(controller);
        }

        internal static IDebugPinService GetDebugPinService(IServiceProvider provider)
        {
            return new DebugPinServiceFactory(provider.GetService<ILogger<DebugPinServiceFactory>>())
                .WithPinOpened(4)
                .WithPinOpened(18)
                .WithPinOpened(20)
                .WithPinOpened(23)
                .WithPinOpened(24)
                .Build();
        }
    }
}
