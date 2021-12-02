using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.ControlUnit
{
    internal class MaintenanceMessagingService : IMaintenanceMessagingService
    {
        private readonly Dictionary<MessageType, List<Func<Task>>> internals = Enum.GetValues<MessageType>().ToList().ToDictionary(x => x, x => new List<Func<Task>>());

        public async Task Publish(MessageType messageType)
        {
            await Task.Run(() => internals[messageType].ForEach(async x => await x()));
        }

        public void Subscribe(Func<Task> action, MessageType messageType)
        {
            internals[messageType].Add(action);
        }

        /// <inheritdoc/>
        public void Unsubscribe(Func<Task> action)
        {
            foreach (var message in internals.Values)
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
