using SmartBohner.ControlUnit.Abstractions;
using System.Device.Gpio;

namespace SmartBohner.ControlUnit
{
    internal class MaintenanceMessagingService : IMaintenanceMessagingService
    {
        internal Dictionary<MessageType, List<Func<PinEventType, Task>>> Callbacks { get; } = Enum.GetValues<MessageType>().ToList().ToDictionary(x => x, x => new List<Func<PinEventType, Task>>());

        public async Task Publish(MessageType messageType, PinEventType pinEventType)
        {
            await Task.Run(() => Callbacks[messageType].ForEach(async x => await x()));
        }

        public void Subscribe(Func<PinEventType, Task> action, MessageType messageType)
        {
            Callbacks[messageType].Add(action);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<PinEventType, Task> action)
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
