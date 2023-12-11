using Microsoft.EntityFrameworkCore;
using OnionSa.Core.Entities;
using System.Reflection;

namespace OnionSa.Infrastructure.Persistence;

public class OnionSaDbContext : DbContext
{
    public OnionSaDbContext(DbContextOptions<OnionSaDbContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
