using Codebridge_TestTask.Data.Configuration;
using Codebridge_TestTask.Entity;
using Microsoft.EntityFrameworkCore;

namespace Codebridge_TestTask.Data;

public class AppDbContext : DbContext
{
    public DbSet<Dog> Dogs => Set<Dog>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DogConfiguration());
        modelBuilder.Entity<Dog>().HasData(new Dog
            { Id = 1, Name = "Neo", Color = "red & amber", TailLenght = 22, Weight = 32 });
        modelBuilder.Entity<Dog>().HasData(new Dog
            { Id = 2, Name = "Jessy", Color = "black & white", TailLenght = 7, Weight = 14 });
    }
}