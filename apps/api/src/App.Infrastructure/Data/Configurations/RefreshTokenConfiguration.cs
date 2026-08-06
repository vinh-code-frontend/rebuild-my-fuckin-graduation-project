namespace App.Api.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Username).IsUnique();
        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Username).IsRequired().HasMaxLength(200);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(user => user.HashedPassword)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(user => user.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(user => user.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(user => user.refreshTokens)
            .WithOne(refreshToken => refreshToken.User)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
