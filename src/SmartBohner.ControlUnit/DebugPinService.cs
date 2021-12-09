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
            if (!controller.IsPinOpen(pin))
            {
                controller.OpenPin(pin);
            }

            controller.Write(pin, PinValue.High);
        }

        public async Task ClosePin(int pin)
        {
            if (!controller.IsPinOpen(pin))
            {
                controller.OpenPin(pin);
            }

            controller.Write(pin, PinValue.Low);
        }

        public async Task<string> GetPin(int pin)
        {
            var pinValue = controller.Read(pin);
            return pinValue.ToString();
        }
    }
}
