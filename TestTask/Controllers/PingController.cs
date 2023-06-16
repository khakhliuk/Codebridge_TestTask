using Microsoft.AspNetCore.Mvc;

namespace Codebridge_TestTask.Controllers;

[Route("ping")]
[ApiController]
public class PingController : Controller
{
    [HttpGet]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok("Dogs house service. Version 1.0.1"));
    }
}