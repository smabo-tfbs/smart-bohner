using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit.Gpio
{
    public interface IGpioChangeContainer : IDisposable
    {
        void Add(int pin, MessageType onChanged);
    }
}