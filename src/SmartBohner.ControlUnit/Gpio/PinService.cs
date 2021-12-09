using SmartBohner.ControlUnit.Abstractions;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Gpio
{
    internal class PinService : IPinService
    {
        private GpioController controller;

        public PinService(GpioController controller)
        {
            this.controller = controller;
        }

        public async Task OpenPin(int pin)
        {
            controller.Write(pin, PinValue.High);
        }

        public async Task ClosePin(int pin)
        {
            controller.Write(pin, PinValue.Low);
        }

        public async Task<string> GetPin(int pin)
        {
            var pinValue = controller.Read(pin);
            return pinValue.ToString();
        }
    }
}
