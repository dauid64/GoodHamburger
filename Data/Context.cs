using Microsoft.EntityFrameworkCore;
using GoodHamburger.Models;

namespace GoodHamburger.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Sandwich> Sandwiches { get; set; } = null!;
    public DbSet<Extra> Extras { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Gerando a seed de dados
        modelBuilder.Entity<Sandwich>().HasData(
            new Sandwich { Id = 1, Name = "X Burger", Price = 5.00m },
            new Sandwich { Id = 2, Name = "X Egg", Price = 4.50m },
            new Sandwich { Id = 3, Name = "X Bacon", Price = 7.00m }
        );

        modelBuilder.Entity<Extra>().HasData(
            new Extra { Id = 1, Name = "Fries", Price = 2.00m },
            new Extra { Id = 2, Name = "Soft Drink", Price = 2.50m }
        );
    }
}