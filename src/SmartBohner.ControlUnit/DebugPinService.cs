using SmartBohner.ControlUnit.Abstractions;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit
{
    internal class DebugPinService : IDebugPinService
    {
        private GpioController controller = new GpioController();

        public async Task OpenPin(int pin)
        {
            controller.OpenPin(pin);
        }

        public async Task ClosePin(int pin)
        {
            controller.ClosePin(pin);
        }

        public async Task<string> GetPin(int pin)
        {
            return controller.GetPinMode(pin).ToString();
        }
    }
}
