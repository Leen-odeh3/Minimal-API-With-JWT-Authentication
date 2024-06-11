using Microsoft.IdentityModel.Tokens;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Abstracts;
public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(User user);
    Task<TokenValidationResult> ValidateTokenAsync(string token);
}
