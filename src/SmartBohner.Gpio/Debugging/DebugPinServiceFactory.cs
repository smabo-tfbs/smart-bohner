using Microsoft.Extensions.Logging;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.Gpio.Debugging
{
    internal class DebugPinServiceFactory : IPinServiceFactory
    {
        private readonly ILogger<DebugPinServiceFactory> logger;
        private readonly ILogger<DebugPinService> debugPinLogger;

        public DebugPinServiceFactory(ILogger<DebugPinServiceFactory> logger, ILogger<DebugPinService> debugPinLogger)
        {
            this.logger = logger;
            this.debugPinLogger = debugPinLogger;
        }

        public IPinService Build()
        {
            return new DebugPinService(debugPinLogger);
        }

        public IPinServiceFactory WithPinAsInput(int pin)
        {
            logger.LogInformation($"Set {pin} to INPUT");
            return this;
        }

        public IPinServiceFactory WithPinAsInput(int pin, Action onChanged)
        {
            logger.LogInformation($"Set {pin} to INPUT");
            return this;
        }

        public IPinServiceFactory WithPinAsOutput(int pin)
        {
            logger.LogInformation($"Set {pin} to OUTPUT");
            return this;
        }
    }
}
