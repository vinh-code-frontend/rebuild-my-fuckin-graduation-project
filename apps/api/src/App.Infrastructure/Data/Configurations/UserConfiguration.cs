
namespace App.Infrastructure.Data.Configurations;

public class UserConfiguration : TimestampWithSoftDeleteEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

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

        // builder.Property(user => user.Status).HasDefaultValue(UserStatus.Active);
        // builder.Property(user => user.Role).HasDefaultValue(UserRole.User);

        builder.HasMany(user => user.refreshTokens)
            .WithOne(refreshToken => refreshToken.User)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
