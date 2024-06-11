using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public ActionResult<string> GetMessage()
    {
        return Ok("Welcome to our API! Thank you for visiting.");
    }
}
