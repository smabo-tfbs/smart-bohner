using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/test/[controller]")]
    public class MessagingTestController : Controller
    {
        private readonly IMaintenanceMessagingService maintenanceMessagingService;

        public MessagingTestController(IMaintenanceMessagingService maintenanceMessagingService)
        {
            this.maintenanceMessagingService = maintenanceMessagingService;
        }


        [HttpGet("notify")]
        public async Task TestNotify()
        {
            await maintenanceMessagingService.Publish(MessageType.NoWater);
        }
    }
}
