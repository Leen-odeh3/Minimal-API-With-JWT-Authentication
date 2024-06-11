using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.Repositories;

public class InMemoryUserRepository : IUserRepo
{
       private readonly List<User> _users = new()
       {
            new User
            {
              UserID = 1,
              FirstName = "leen",
              LastName = "odeh",
              Username = "leen_3",
              Password = "test1"
            },
            new User
            {
              UserID = 2,
              FirstName = "sara",
              LastName = "mohammad",
              Username = "sara221",
              Password = "test2"
            }
       };

        public Task<User?> Get(string username, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(u => username == u.Username && password == u.Password));
        }
}