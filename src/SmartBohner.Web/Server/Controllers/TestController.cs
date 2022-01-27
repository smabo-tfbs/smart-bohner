using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;
using SmartBohner.Web.Server.Hubs;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/test/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IPinService debugPinService;

        private readonly IMaintenanceMessagingService _messagingService;

        public TestController(IPinServiceFactory debugPinService, IMaintenanceMessagingService messagingService)
        {
            this.debugPinService = debugPinService.Build();
            _messagingService = messagingService;
        }

        [HttpGet("SignalR")]
        public async Task TestSignalR(MessageType messageType)
        {
            await _messagingService.Publish(messageType, PinEventType.Rising);
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "HelloDocker !!!!!!";
        }

        [HttpGet("pin/{pin}")]
        public Task<string> GetPin(int pin)
        {
            return debugPinService.GetPin(pin);
        }

        [HttpGet("pin/{pin}/on")]
        public Task SetPinOn(int pin)
        {
            return debugPinService.OpenPin(pin);
        }

        [HttpGet("pin/{pin}/off")]
        public Task SetPinOff(int pin)
        {
            return debugPinService.ClosePin(pin);
        }

    }
}
