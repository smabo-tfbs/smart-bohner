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

        /// <summary>
        /// Turn on coffee machine
        /// </summary>
        /// <returns></returns>
        [HttpGet("Power/On")]
        public async Task On()
        {
            if (!await _coffeeMachineService.IsOn())
            {
                await _coffeeMachineService.Start();
            }
        }

        /// <summary>
        /// Turn off coffee machine
        /// </summary>
        /// <returns></returns>
        [HttpGet("Power/Off")]
        public async Task Off()
        {
            if (await _coffeeMachineService.IsOn())
            {
                await _coffeeMachineService.Shutdown();
            }
        }

        /// <summary>
        /// Get coffee machine's status
        /// </summary>
        /// <returns>True: if coffee machine is on; otherwise false</returns>
        [HttpGet("Power/Status")]
        public async Task<bool> GetStatus()
        {
            return await _coffeeMachineService.IsOn();
        }
    }
}
