namespace App.Infrastructure.Data.Configurations;

public abstract class TimestampEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : TimestampEntity
{
    protected static readonly ValueConverter<DateTime, DateTime> UtcDateTimeConverter = new(
        toDb => toDb.Kind == DateTimeKind.Utc ? toDb : toDb.ToUniversalTime(),
        fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc)
    );

    protected static readonly ValueConverter<DateTime?, DateTime?> NullableUtcDateTimeConverter = new(
        toDb => toDb.HasValue
            ? toDb.Value.Kind == DateTimeKind.Utc
                ? toDb.Value
                : toDb.Value.ToUniversalTime()
            : toDb,
        fromDb => fromDb.HasValue
            ? DateTime.SpecifyKind(fromDb.Value, DateTimeKind.Utc)
            : fromDb
    );

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasConversion(UtcDateTimeConverter)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt)
            .HasConversion(NullableUtcDateTimeConverter)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
