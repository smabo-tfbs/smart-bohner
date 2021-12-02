using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class MaintenanceService : IMaintenanceService
    {

        /// <inheritdoc/>
        public async Task<bool> Alarm()
        {
            return false;
        }

        /// <inheritdoc/>
        public Task ExecuteCalcClean()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<bool> NeedsCalcClean()
        {
            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> NoBeans()
        {
            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> NoWater()
        {
            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> WasteFull()
        {
            return false;
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
    }
}
