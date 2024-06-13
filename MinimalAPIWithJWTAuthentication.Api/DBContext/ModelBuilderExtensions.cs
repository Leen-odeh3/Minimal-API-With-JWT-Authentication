using Microsoft.EntityFrameworkCore;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.DBContext;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { UserID = 1, FirstName = "leen", LastName = "odeh", Username = "leenodeh3", Password = "test123" },
            new User { UserID = 2, FirstName = "Jane", LastName = "Doe", Username = "jane.doe", Password = "password2" },
            new User { UserID = 3, FirstName = "Alice", LastName = "Smith", Username = "alice.smith", Password = "password3" },
            new User { UserID = 4, FirstName = "Bob", LastName = "Johnson", Username = "bob.johnson", Password = "password4" },
            new User { UserID = 5, FirstName = "Eve", LastName = "Brown", Username = "eve.brown", Password = "password5" }
        );
    }
}
