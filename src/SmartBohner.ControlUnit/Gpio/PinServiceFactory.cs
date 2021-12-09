using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using System.Device.Gpio;

namespace SmartBohner.ControlUnit.Gpio
{
    /// <summary>
    /// Creates a new instance of the <see cref="IPinService"/>
    /// </summary>
    public class PinServiceFactory
    {
        private readonly ILogger<PinServiceFactory> logger;
        private readonly GpioController controller = new();

        /// <summary>
        /// Creates a new instance of the <see cref="PinServiceFactory"/>-class.
        /// </summary>
        /// <param name="logger"></param>
        public PinServiceFactory(ILogger<PinServiceFactory> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Specifies that a pin is opened for output-signals
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public PinServiceFactory WithPinOpened(int pin)
        {
            try
            {
                controller.OpenPin(pin, PinMode.Output);
                logger.LogInformation($"Opened pin: {pin}");
            }
            catch (Exception)
            {
                logger.LogWarning($"Error opening pin: {pin}");
            }

            return this;
        }

        /// <summary>
        /// Creates the <see cref="IPinService"/>-object
        /// </summary>
        /// <returns></returns>
        public IPinService Build()
        {
            return new PinService(controller);
        }

        /// <summary>
        /// Creates a default-instance with some ports opened
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        internal static IPinService GetDebugPinService(IServiceProvider provider)
        {
            return new PinServiceFactory(provider.GetService<ILogger<PinServiceFactory>>())
                .WithPinOpened(4)
                .WithPinOpened(18)
                .WithPinOpened(20)
                .WithPinOpened(23)
                .WithPinOpened(24)
                .Build();
        }
    }
}
