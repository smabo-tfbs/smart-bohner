using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Abstractions.Contracts;
using System.Device.Gpio;

namespace SmartBohner.ControlUnit
{
    internal class MaintenanceMessagingService : IMaintenanceMessagingService
    {
        internal Dictionary<MessageType, List<Func<PinEvent, Task>>> Callbacks { get; } = Enum.GetValues<MessageType>().ToList().ToDictionary(x => x, x => new List<Func<PinEvent, Task>>());

        public async Task Publish(MessageType messageType, PinEventType pinEventType)
        {
            await Task
                .Run(() => Callbacks[messageType]
                .ForEach(async x => await x(new PinEvent(messageType, pinEventType))));
        }

        public void Subscribe(Func<PinEvent, Task> action, MessageType messageType)
        {
            Callbacks[messageType].Add(action);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<PinEvent, Task> action)
        {
            foreach (var message in Callbacks.Values)
            {
                if (message.Contains(action))
                {
                    message.Remove(action);
                    return;
                }
            }
        }
    }
}
