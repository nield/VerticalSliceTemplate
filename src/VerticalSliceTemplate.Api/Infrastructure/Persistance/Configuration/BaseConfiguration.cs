using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VerticalSliceTemplate.Api.Infrastructure.Persistance.Configuration;

[ExcludeFromCodeCoverage]
public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    private static readonly IList<TEntity> EmptySeed = [];

    public abstract string TableName { get; }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.HasData(SeedData());
    }

    protected virtual IList<TEntity> SeedData() => EmptySeed;
}