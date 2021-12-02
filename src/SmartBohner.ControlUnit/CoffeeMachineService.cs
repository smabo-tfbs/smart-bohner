using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class CoffeeMachineService : ICoffeeMachineService
    { 
        /// <inheritdoc/>
        public async Task<bool> IsOn()
        {
            return false;
        }

        /// <inheritdoc/>
        public Task Shutdown()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task Start()
        {
            return Task.CompletedTask;
        }
    }
}
