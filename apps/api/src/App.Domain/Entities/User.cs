namespace App.Domain.Entities;

public class User : TimestampWithSoftDeleteEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public ICollection<RefreshToken> refreshTokens { get; set; } = new List<RefreshToken>();

}