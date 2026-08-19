using WanderKiwi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WanderKiwi.Infrastructure.Data;

public class WanderKiwiDbContext : DbContext
{
    public WanderKiwiDbContext(DbContextOptions<WanderKiwiDbContext> options)
        : base(options)
    {
    }

    // Represents the Attractions table in SQL Server
    public DbSet<Attraction> Attractions { get; set; }
}