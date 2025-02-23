﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Configurations;
using MinimalAPIWithJWTAuthentication.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace MinimalAPIWithJWTAuthentication.Api.Services;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtAuthenticationConfig _jwtTokenOptions;
    private readonly UserService _userService;

    public JwtTokenGenerator(IOptions<JwtAuthenticationConfig> jwtTokenOptions, UserService userService)
    {
        _jwtTokenOptions = jwtTokenOptions.Value ?? throw new ArgumentNullException(nameof(jwtTokenOptions));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    public async Task<string> GenerateTokenAsync(AuthRequestBody userAuth)
    {
        var isValidUser = await _userService.ValidateUserCredentialsAsync(userAuth.Username, userAuth.Password);

        if (!isValidUser)
        {
            return null;
        }

        var user = await _userService.GetByUserNameAsync(userAuth.Username);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
        };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(_jwtTokenOptions.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtTokenOptions.Issuer,
            _jwtTokenOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<TokenValidationResult> ValidateTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_jwtTokenOptions.SecretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtTokenOptions.Issuer,
            ValidAudience = _jwtTokenOptions.Audience,
            IssuerSigningKey = key
        };

        return await tokenHandler.ValidateTokenAsync(token, validationParameters);
    }
}