using Microsoft.Extensions.Logging;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.Gpio.Debugging
{
    internal class DebugPinService : IPinService
    {
        private readonly ILogger<DebugPinService> logger;

        public DebugPinService(ILogger<DebugPinService> logger)
        {
            this.logger = logger;
        }
        public Task ClosePin(int pin)
        {
            logger.LogInformation($"{pin} changed to down");
            return Task.CompletedTask;
        }

        public Task<string> GetPin(int pin)
        {
            logger.LogInformation($"Not supported");
            return Task.FromResult(Pin.High);
        }

        public Task OpenPin(int pin)
        {
            logger.LogInformation($"{pin} changed to up");
            return Task.CompletedTask;
        }
    }
}
