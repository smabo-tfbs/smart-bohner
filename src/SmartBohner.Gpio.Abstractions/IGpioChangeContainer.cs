using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Gpio.Abstractions
{
    public interface IGpioChangeContainer : IDisposable
    {
        /// <summary>
        /// Adds a new pin with its type to the messaging-system
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="messageType"></param>
        void Add(int pin, MessageType onChanged);
    }
}