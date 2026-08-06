namespace App.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string HashedToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public User? User { get; set; }
}