using SmartBohner.ControlUnit.Gpio;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Extensions
{
    /// <summary>
    /// This class is a singleton holding all callbacks for pin-changings
    /// </summary>
    public class GpioChangeContainer : IGpioChangeContainer
    {
        private GpioController controller = new();

        public void Add(int pin, Action onChanged)
        {
            if (!controller.IsPinOpen(pin))
            {
                controller.OpenPin(pin);
            }

            controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, (x, y) => onChanged());
        }

        public void Dispose()
        {
            controller.Dispose();
        }
    }
}
