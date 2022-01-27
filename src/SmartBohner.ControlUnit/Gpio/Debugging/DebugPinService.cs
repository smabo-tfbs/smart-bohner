using SmartBohner.ControlUnit.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Gpio.Debugging
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
            return Task.FromResult("NOT SUPPORTED");
        }

        public Task OpenPin(int pin)
        {
            logger.LogInformation($"{pin} changed to up");
            return Task.CompletedTask;
        }
    }
}
