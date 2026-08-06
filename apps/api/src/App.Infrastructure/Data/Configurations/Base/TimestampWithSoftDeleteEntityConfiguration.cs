namespace App.Infrastructure.Data.Configurations;

public abstract class TimestampWithSoftDeleteEntityConfiguration<TEntity> : TimestampEntityConfiguration<TEntity>
    where TEntity : TimestampWithSoftDeleteEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.DeletedAt);
    }
}
