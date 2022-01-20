using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using System.Device.Gpio;
using System.Security.Cryptography.X509Certificates;

namespace SmartBohner.ControlUnit.Gpio
{
    /// <summary>
    /// Creates a new instance of the <see cref="IPinService"/>
    /// </summary>
    public class PinServiceFactory
    {
        private readonly ILogger<PinServiceFactory> logger;
        private readonly List<Pin> pins = new List<Pin>();

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
        public PinServiceFactory WithPinAsOutput(int pin)
        {
            RegisterInternal(pin, PinMode.Output);
            return this;
        }

        /// <summary>
        /// Specified that a pin is opened for input-signals
        /// </summary>
        /// <param name="pin">The pin to set</param>
        /// <returns></returns>
        public PinServiceFactory WithPinAsInput(int pin)
        {
            RegisterInternal(pin, PinMode.Input);
            return this;
        }

        public PinServiceFactory WithPinAsInput(int pin, Action onChanged)
        {
            RegisterInternal(pin, PinMode.Input, onChanged);
            return this;
        }

        private void RegisterInternal(int pin, PinMode mode, Action? onChanged = null)
        {
            if (pins.Any(x => x.Number == pin))
            {
                throw new InvalidOperationException("pin already added");
            }

            pins.Add(new(pin, mode, onChanged));
            logger.LogInformation($"Added {pin} set to {mode}");
        }

        /// <summary>
        /// Creates the <see cref="IPinService"/>-object
        /// </summary>
        /// <returns></returns>
        public IPinService Build()
        {
            var controller = new GpioController();
            var exceptions = new List<Exception>();

            foreach (Pin pin in pins)
            {
                try
                {
                    controller.SetPinMode(pin.Number, pin.Mode);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            return exceptions.Any()
                ? throw new AggregateException(exceptions)
                : new PinService(controller);
        }

        private record Pin(int Number, PinMode Mode, Action? onChanged = null);
    }
}
