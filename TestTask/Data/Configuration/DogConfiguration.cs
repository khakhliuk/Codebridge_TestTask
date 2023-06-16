using Codebridge_TestTask.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebridge_TestTask.Data.Configuration;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable(nameof(Dog), "general").HasKey(o => o.Id);
        builder.Property(x => x.Name).HasColumnType("varchar(60)");
        builder.Property(x => x.Color).HasColumnType("varchar(60)");
    }
}