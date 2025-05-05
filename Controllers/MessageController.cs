using Microsoft.AspNetCore.Mvc;

namespace CSharpApiBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from C# API!");
        }
    }
}
