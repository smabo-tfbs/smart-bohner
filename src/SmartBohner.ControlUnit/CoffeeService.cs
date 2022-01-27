using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class CoffeeService : ICoffeeService
    {
        private readonly IPinSwitcher pinSwitcher;

        public CoffeeService(IPinSwitcher pinSwitcher)
        {
            this.pinSwitcher = pinSwitcher;
        }
        /// <inheritdoc/>
        public Task Coffee()
        {
            return pinSwitcher.Send2Port(27);
        }

        /// <inheritdoc/>
        public Task Espresso()
        {
            return pinSwitcher.Send2Port(17);
        }

        /// <inheritdoc/>
        public Task Lungo()
        {
            return pinSwitcher.Send2Port(18);
        }
    }
}
