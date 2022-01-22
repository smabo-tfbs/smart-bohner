using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SmartBohner.Web.Shared.SignalR;

namespace SmartBohner.Web.Client.Infrastructure
{
    public interface IWarningHubConnectionHandler : IAsyncDisposable
    {
        Task Start();
        Task Stop();

        void Listen(Action<WarningResult> action);
    }

    public sealed class WarningHubConnectionHandler : IWarningHubConnectionHandler
    {
        private readonly HubConnection _hubConnection;

        public WarningHubConnectionHandler(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/warnings"))
                .WithAutomaticReconnect()
                .Build();
        }

        public Task Start() => _hubConnection.StartAsync();
        public Task Stop() => _hubConnection.StopAsync();

        public void Listen(Action<WarningResult> action)
        {
            _hubConnection.On<WarningResult>(SignalRClient.ReceiveWarnings, result =>
            {
                action(result);
            });
        }

        public async ValueTask DisposeAsync() => await Stop();
    }
}
