using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;

namespace SmartBohner.ControlUnit
{
    internal class CoffeeService : ICoffeeService
    {
        private readonly PinServiceFactory pinServiceFactory;
        private readonly ILogger<CoffeeService> logger;

        public CoffeeService(PinServiceFactory pinServiceFactory, ILogger<CoffeeService> logger)
        {
            this.pinServiceFactory = pinServiceFactory;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task Coffee()
        {
            logger.LogInformation("Start making coffee");
            var pin = pinServiceFactory.Build();
           
        }

        /// <inheritdoc/>
        public Task Espresso()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task Lungo()
        {
            return Task.CompletedTask;
        }
    }
}
