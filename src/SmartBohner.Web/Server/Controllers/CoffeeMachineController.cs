using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineService _coffeeMachineService;
        private readonly ILogger<CoffeeMachineController> logger;

        public CoffeeMachineController(ICoffeeMachineService coffeeMachineService, ILogger<CoffeeMachineController> logger)
        {
            _coffeeMachineService = coffeeMachineService;
            this.logger = logger;
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
                logger.LogInformation("Started coffee machine");
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
                logger.LogInformation("Shutdown coffee machine");
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
