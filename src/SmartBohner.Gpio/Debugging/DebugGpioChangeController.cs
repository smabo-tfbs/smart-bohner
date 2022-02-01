using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions.Contracts;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.Gpio.Debugging
{
    internal class DebugGpioChangeController : IGpioChangeContainer
    {
        private readonly ILogger<DebugGpioChangeController> logger;

        public DebugGpioChangeController(ILogger<DebugGpioChangeController> logger)
        {
            this.logger = logger;
        }

        public void Add(int pin, MessageType eventType)
        {
            logger.LogInformation($"{pin} registered for change-tracking");
        }

        public void Dispose()
        {
            logger.LogInformation($"DISPOSED {nameof(DebugGpioChangeController)}");
        }
    }
}
