using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Gpio.Debugging
{
    internal class DebugPinServiceFactory : IPinServiceFactory
    {
        IServiceProvider serviceCollection;
        private readonly ILogger<DebugPinServiceFactory> logger;

        public DebugPinServiceFactory(IServiceProvider serviceCollection, ILogger<DebugPinServiceFactory> logger)
        {
            this.serviceCollection = serviceCollection;
            this.logger = logger;
        }

        public IPinService Build()
        {
            return serviceCollection.GetService<IPinService>();
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
