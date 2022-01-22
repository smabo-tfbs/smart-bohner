using Microsoft.AspNetCore.SignalR;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Web.Shared;

namespace SmartBohner.Web.Server.Hubs
{
    public interface IWarningHubNotifier : IDisposable
    {
        Task Notify(PinEventType pinEventType, MessageType messageType);
    }

    public class WarningHubNotifier : IWarningHubNotifier
    {
        private readonly IHubContext _context;
        private readonly IMaintenanceMessagingService _messagingService;

        public WarningHubNotifier(IHubContext context, IMaintenanceMessagingService messagingService)
        {
            _context = context;
            _messagingService = messagingService;

            Subscribe();
        }

        public async Task Notify(PinEventType pinEventType, MessageType messageType)
        {
            await _context.Clients.All.SendAsync(SignalRClient.ReceiveWarnings, pinEventType, messageType);
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            foreach (var value in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
            {
                _messagingService.Subscribe(async x => await Notify(x, value), value);
            }
        }

        private void Unsubscribe()
        {
            foreach (var value in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
            {
                _messagingService.Unsubscribe(async x => await Notify(x, value));
            }
        }
    }
}
