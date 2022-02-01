using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Abstractions.Contracts;
using SmartBohner.Gpio.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/test/[controller]")]
    public class MessagingTestController : Controller
    {
        private readonly IMaintenanceMessagingService maintenanceMessagingService;
        private readonly IPinSwitcher pinSwitcher;

        public MessagingTestController(IMaintenanceMessagingService maintenanceMessagingService, IPinSwitcher pinSwitcher)
        {
            this.maintenanceMessagingService = maintenanceMessagingService;
            this.pinSwitcher = pinSwitcher;
        }


        [HttpGet("notify")]
        public async Task TestNotify(MessageType type, PinEventType eventType)
        {
            await maintenanceMessagingService.Publish(type, eventType);
        }

        [HttpGet("button")]
        public async Task TestButton(int button, int intervall)
        {
            await pinSwitcher.Send2Port(button, intervall);
        }
    }
}
