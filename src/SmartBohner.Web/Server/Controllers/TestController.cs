using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IPinService debugPinService;

        public TestController(IPinService debugPinService)
        {
            this.debugPinService = debugPinService;
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
