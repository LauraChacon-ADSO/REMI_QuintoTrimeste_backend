using Microsoft.AspNetCore.Mvc;

namespace Proyecto_REMI_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API funcionando");
        }
    }
}
