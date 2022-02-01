using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class MaintenanceService : IMaintenanceService
    {
        private readonly IPinService pinService;

        public MaintenanceService(IPinService pinService)
        {
            this.pinService = pinService;
        }

        /// <inheritdoc/>
        public Task<bool> Alarm()
        {
            return PinIsHigh(21);
        }

        /// <inheritdoc/>
        public Task<bool> NeedsCalcClean()
        {
            return PinIsHigh(12);
        }

        /// <inheritdoc/>
        public Task<bool> NoBeans()
        {
            return PinIsHigh(20);
        }

        /// <inheritdoc/>
        public Task<bool> NoWater()
        {
            return PinIsHigh(16);
        }

        /// <inheritdoc/>
        public Task<bool> WasteFull()
        {
            return PinIsHigh(26);
        }

        /// <inheritdoc/>
        public async Task<bool> HasAny()
        {
            return
                await Alarm()
                || await NoBeans()
                || await NoWater()
                || await WasteFull();
        }

        private async Task<bool> PinIsHigh(int pin)
        {
            return await pinService.GetPin(pin) == Pin.High;
        }
    }
}
