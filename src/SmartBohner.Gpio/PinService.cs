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

        public async Task<PinInfo> GetPin(int pin)
        {
            var pinValue = controller.Read(pin);

            if (pinValue == PinValue.High)
            {
                return PinInfo.HighPin;
            }
            else if (pinValue == PinValue.Low)
            {
                return PinInfo.LowPin;
            }
            else
            {
                throw new InvalidOperationException($"Not know PinValue: {pinValue.ToString} on pin {pin}");
            }
        }
    }
}
