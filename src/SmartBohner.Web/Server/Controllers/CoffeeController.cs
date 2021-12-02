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

        [HttpGet("Espresso")]
        public async Task Espresso()
        {
            await _coffeeService.Espresso();
        }

        [HttpGet("Lungo")]
        public async Task Lungo()
        {
            await _coffeeService.Lungo();
        }

        [HttpGet("Coffee")]
        public async Task Coffee()
        {
            await _coffeeService.Coffee();
        }
    }
}
