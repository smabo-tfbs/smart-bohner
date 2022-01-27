using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Shared.SignalR
{
    public class WarningResult
    {
        public PinEventType PinEventType { get; set; }
        public MessageType MessageType { get; set; }
    }
}
