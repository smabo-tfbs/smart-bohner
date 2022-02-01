using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class SpecialActionService : ISpecialActionService
    {
        private readonly IPinSwitcher pinSwitcher;
        private readonly IPinService pinService;

        public SpecialActionService(
            IPinSwitcher pinSwitcher, 
            IPinServiceFactory pinServiceFactory)
        {
            this.pinSwitcher = pinSwitcher;
            this.pinService = pinServiceFactory.Build();
        }

        public Task ExecuteCalcClean()
        {
            return pinSwitcher.Send2Port(22, 3500);
        }

        public Task ExecuteHotWater()
        {
            return pinSwitcher.Send2Port(22);
        }

        public Task ExecuteSteam()
        {
            // Erfordert mehr als nur Knopfdruck
            // Wird "geskippt"
            throw new NotSupportedException();
        }
        
        public async Task ToggleAromaPlus(bool newValue)
        {
            var pinStatus = await pinService.GetPin(7);
            var needChange = pinStatus == Pin.High == newValue;

            if (needChange)
            {
                await pinSwitcher.Send2Port(14);
            }
        }
    }
}
