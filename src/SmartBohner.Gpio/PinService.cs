using SmartBohner.Gpio.Abstractions;
using System.Device.Gpio;

namespace SmartBohner.Gpio
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

            if (pinValue == PinValue.High)
            {
                return Pin.High;
            }
            else if (pinValue == PinValue.Low)
            {
                return Pin.Low;
            }
            else
            {
                throw new InvalidOperationException($"Not know PinValue: {pinValue.ToString} on pin {pin}");
            }
        }
    }
}
