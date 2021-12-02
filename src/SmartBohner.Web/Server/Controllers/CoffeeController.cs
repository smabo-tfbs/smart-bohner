using Microsoft.AspNetCore.Mvc;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        [HttpGet]
        public bool Test()
        {
            return true;
        }
    }
}
