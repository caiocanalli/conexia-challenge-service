using Microsoft.AspNetCore.Mvc;

namespace Conexia.Challenge.Services.Api.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok();
    }
}