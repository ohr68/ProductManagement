using Microsoft.EntityFrameworkCore;

namespace Auth.ORM.Context;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}