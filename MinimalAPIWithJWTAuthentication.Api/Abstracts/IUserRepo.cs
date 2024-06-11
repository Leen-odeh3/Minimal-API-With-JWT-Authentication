using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Abstracts;

public interface IUserRepo : IRepo<User>
{
    Task<bool> ValidateUserCredentialsAsync(string username, string password);
    Task<User> GetUserByUsernameAsync(string username);
}
