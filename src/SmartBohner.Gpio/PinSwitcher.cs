using SmartBohner.Gpio.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.Gpio
{
    public class PinSwitcher : IPinSwitcher
    {
        private readonly IPinServiceFactory pinServiceFactory;

        public PinSwitcher(IPinServiceFactory pinServiceFactory)
        {
            this.pinServiceFactory = pinServiceFactory;
        }

        public async Task Send2Port(int pin, int interval = 500)
        {
            var pinService = pinServiceFactory.Build();

            await pinService.OpenPin(pin);
            await Task.Delay(interval);
            await pinService.ClosePin(pin);
        }
    }
}
