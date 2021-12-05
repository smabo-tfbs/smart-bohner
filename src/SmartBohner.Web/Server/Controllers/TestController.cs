using Microsoft.AspNetCore.Mvc;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            return "HelloDocker!!!";
        }
    }
}
