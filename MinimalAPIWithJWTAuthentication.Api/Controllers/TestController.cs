using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;
public class TestController
{
    [HttpGet]
    public ActionResult<string> GetMessage()
    {
        return Ok("Welcome to our API! Thank you for visiting.");
    }

}
