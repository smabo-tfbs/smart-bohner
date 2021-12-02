namespace SmartBohner.ControlUnit.Abstractions
{
    public interface IMaintenanceMessagingService
    {
        /// <summary>
        /// Subscribes a new Action with the provided Action and message-type
        /// </summary>
        /// <remarks>
        /// DONT SUBSCRIBE DELEGATES: Only subsrice real methods
        /// </remarks>
        /// <param name="action"></param>
        /// <param name="messageType"></param>
        void Subscribe(Action action, MessageType messageType);

        /// <summary>
        /// Unsubscribes the action from the messaging system
        /// </summary>
        /// <param name="action">The action to unsubscribe</param>
        void Unsubscribe(Action action);

        /// <summary>
        /// Publishes a new Message with the provided <see cref="MessageType"/>
        /// </summary>
        /// <param name="messageType">The message-type</param>
        /// <returns></returns>
        Task Publish(MessageType messageType);
    }
}
