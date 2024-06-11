using Microsoft.AspNetCore.Mvc;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;


public class AuthenticationController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepo _userRepository;

    public AuthenticationController(IUserRepo userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> Authenticate(AuthRequestBody authenticationRequestBody)
    {
        var user = await _userRepository.Get(authenticationRequestBody.Username, authenticationRequestBody.Password);

        if (user is null) return Unauthorized();

        return Ok(_jwtTokenGenerator.GenerateToken(user));
    }
}