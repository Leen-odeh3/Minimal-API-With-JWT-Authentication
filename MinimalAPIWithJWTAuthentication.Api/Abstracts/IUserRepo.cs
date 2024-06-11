using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Abstracts;

public interface IUserRepo
{
    Task<User?> Get(string userName, string password);

}
