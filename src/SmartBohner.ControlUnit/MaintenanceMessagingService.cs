using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal     class MaintenanceMessagingService : IMaintenanceMessagingService
    {
        internal Dictionary<MessageType, List<Func<Task>>> Callbacks { get; } = Enum.GetValues<MessageType>().ToList().ToDictionary(x => x, x => new List<Func<Task>>());

        public async Task Publish(MessageType messageType)
        {
            await Task.Run(() => Callbacks[messageType].ForEach(async x => await x()));
        }

        public void Subscribe(Func<Task> action, MessageType messageType)
        {
            Callbacks[messageType].Add(action);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<Task> action)
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
