namespace SmartBohner.ControlUnit.Abstractions.Contracts
{
    public class PinEvent
    {
        public PinEvent(MessageType messageType, PinEventType eventType)
        {
            MessageType = messageType;
            EventType = eventType;
        }

        public PinEventType EventType { get; }

        public MessageType MessageType { get; }
    }
}
