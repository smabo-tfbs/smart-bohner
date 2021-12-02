using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class CoffeeService : ICoffeeService
    {
        /// <inheritdoc/>
        public Task Coffee()
        {
            return Task.CompletedTask;
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
