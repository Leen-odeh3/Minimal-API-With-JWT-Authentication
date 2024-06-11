using Microsoft.AspNetCore.Mvc;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenGenerator;

    public AuthController(ITokenService tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] User input)
    {
        var token = await _tokenGenerator.GenerateTokenAsync(input);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}