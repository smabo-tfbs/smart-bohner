using Microsoft.AspNetCore.SignalR;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Abstractions.Contracts;
using SmartBohner.Web.Shared.SignalR;

namespace SmartBohner.Web.Server.Hubs
{
    public interface IWarningHubNotifier : IDisposable
    {
        Task Notify(PinEvent pinEvent);
    }

    public class WarningHubNotifier : IWarningHubNotifier
    {
        private readonly IHubContext<WarningHub> _context;
        private readonly IMaintenanceMessagingService _messagingService;

        public WarningHubNotifier(IHubContext<WarningHub> context, IMaintenanceMessagingService messagingService)
        {
            _context = context;
            _messagingService = messagingService;

            Subscribe();
        }

        public async Task Notify(PinEvent pinEvent)
        {
            await _context.Clients.All.SendAsync(SignalRClient.ReceiveWarnings, new WarningResult
            {
                PinEventType = pinEvent.EventType,
                MessageType = pinEvent.MessageType
            });

        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            foreach (var value in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
            {
                _messagingService.Subscribe(Notify, value);
            }
        }

        private void Unsubscribe()
        {
            foreach (var value in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
            {
                _messagingService.Unsubscribe(Notify);
            }
        }
    }
}
