using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Web.Shared.SignalR;

namespace SmartBohner.Web.Client.Infrastructure.Mock
{
    public class MockWarningHubConnectionHandler : IWarningHubConnectionHandler
    {
        private Timer _timer;

        public void Listen(Action<WarningResult> action)
        {
            _timer = new Timer(x =>
            {
                action(new WarningResult
                {
                    PinEventType = PinEventType.Rising,
                    MessageType = MessageType.NoWater
                });
            }, new AutoResetEvent(false), 2000, 5000);
        }

        public Task Start() => Task.CompletedTask;
        public Task Stop() => Task.CompletedTask;
        public ValueTask DisposeAsync() => _timer.DisposeAsync();
    }
}
