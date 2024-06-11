using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Abstracts;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);

    bool ValidateToken(string token);
}
