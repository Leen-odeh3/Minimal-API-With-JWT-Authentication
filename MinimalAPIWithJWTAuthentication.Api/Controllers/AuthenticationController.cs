using Microsoft.AspNetCore.Mvc;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    public AuthenticationController(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
        
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] AuthRequestBody input)
    {
        var token = await _tokenGenerator.GenerateTokenAsync(input);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}