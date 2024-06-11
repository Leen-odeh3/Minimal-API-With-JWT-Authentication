using Microsoft.EntityFrameworkCore;
using MinimalAPIWithJWTAuthentication.Api.Models;

namespace MinimalAPIWithJWTAuthentication.Api.DBContext;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
