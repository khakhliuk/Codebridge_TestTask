using Codebridge_TestTask.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Codebridge_TestTask.Interfaces;

public interface IAppDbContext
{
    public DbSet<Dog> Dogs { get; set; }
    int SaveChanges();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}