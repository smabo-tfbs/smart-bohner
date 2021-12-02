using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineService _coffeeMachineService;

        public CoffeeMachineController(ICoffeeMachineService coffeeMachineService)
        {
            _coffeeMachineService = coffeeMachineService;
        }

        [HttpGet("Power/On")]
        public async Task On()
        {
            if (!await _coffeeMachineService.IsOn())
            {
                await _coffeeMachineService.Start();
            }
        }

        [HttpGet("Power/Off")]
        public async Task Off()
        {
            if (await _coffeeMachineService.IsOn())
            {
                await _coffeeMachineService.Shutdown();
            }
        }
    }
}
