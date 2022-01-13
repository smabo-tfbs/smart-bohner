namespace SmartBohner.ControlUnit.Abstractions
{
    public interface IMaintenanceMessagingService
    {
        /// <summary>
        /// Subscribes a new Action with the provided Action and message-type
        /// </summary>
        /// <remarks>
        /// DONT SUBSCRIBE DELEGATES: Only subscribe real methods
        /// </remarks>
        /// <param name="action"></param>
        /// <param name="messageType"></param>
        void Subscribe(Func<PinEventType, Task> action, MessageType messageType);

        /// <summary>
        /// Unsubscribes the action from the messaging system
        /// </summary>
        /// <param name="action">The action to unsubscribe</param>
        void Unsubscribe(Func<PinEventType, Task> action);

        /// <summary>
        /// Publishes a new Message with the provided <see cref="MessageType"/>
        /// </summary>
        /// <param name="messageType">The message-type</param>
        /// <returns></returns>
        Task Publish(MessageType messageType, PinEventType pinEventType);
    }
}
