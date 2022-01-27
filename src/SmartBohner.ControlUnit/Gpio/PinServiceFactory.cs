using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using System.Device.Gpio;

namespace SmartBohner.ControlUnit.Gpio
{
    /// <summary>
    /// Creates a new instance of the <see cref="IPinService"/>
    /// </summary>
    internal class PinServiceFactory : IPinServiceFactory
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

        /// <inheritdoc/>
        public IPinServiceFactory WithPinAsOutput(int pin)
        {
            RegisterInternal(pin, PinMode.Output);
            return this;
        }

        /// <inheritdoc>/>
        public IPinServiceFactory WithPinAsInput(int pin)
        {
            RegisterInternal(pin, PinMode.Input);
            return this;
        }

        /// <inheritdoc/>
        public IPinServiceFactory WithPinAsInput(int pin, Action onChanged)
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

        /// <inheritdoc/>
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

        private record Pin(int Number, PinMode Mode, Action? OnChanged = null);
    }
}
