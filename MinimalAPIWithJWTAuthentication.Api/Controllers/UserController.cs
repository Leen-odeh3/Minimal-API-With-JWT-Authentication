using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Services;
using System.Security.Claims;

namespace MinimalAPIWithJWTAuthentication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IJwtTokenGenerator _tokenService;

    public UserController(UserService userService, IJwtTokenGenerator tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        var principal = await _tokenService.ValidateTokenAsync(token);

        if (principal is null)
        {
            return Unauthorized();
        }

        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }


    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(int id)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int authenticatedUserId))
        {
            return Unauthorized();
        }

        if (id != authenticatedUserId)
        {
            return Forbid();
        }

        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
