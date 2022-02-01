using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly IPinSwitcher pinSwitcher;
        private readonly ILogger<CoffeeMachineService> logger;

        public CoffeeMachineService(IPinSwitcher pinSwitcher, ILogger<CoffeeMachineService> logger)
        {
            this.pinSwitcher = pinSwitcher;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> IsOn()
        {
            return false;
        }

        /// <inheritdoc/>
        public async Task Shutdown()
        {
            if (await IsOn())
            {
                logger.LogInformation("Shutdown started");
                await pinSwitcher.Send2Port(4);
            }
            else
            {
                logger.LogInformation("Machine is already off. No shutdown needed");
            }
        }

        /// <inheritdoc/>
        public async Task Start()
        {
            if (!await IsOn())
            {
                logger.LogInformation("Starting machine");
                await pinSwitcher.Send2Port(4);
            }
            else
            {
                logger.LogInformation("Machine is already on. No start needed");
            }
        }
    }
}
