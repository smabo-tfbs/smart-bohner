using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        /// <summary>
        /// Start espresso brewing process
        /// </summary>
        /// <returns></returns>
        [HttpGet("Espresso")]
        public async Task Espresso()
        {
            await _coffeeService.Espresso();
        }

        /// <summary>
        /// Start lungo brewing process
        /// </summary>
        /// <returns></returns>
        [HttpGet("Lungo")]
        public async Task Lungo()
        {
            await _coffeeService.Lungo();
        }
        
        /// <summary>
        /// Start coffee brewing process
        /// </summary>
        /// <returns></returns>
        [HttpGet("Coffee")]
        public async Task Coffee()
        {
            await _coffeeService.Coffee();
        }
    }
}
