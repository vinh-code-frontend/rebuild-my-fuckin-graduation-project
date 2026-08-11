namespace App.Application.DTOs;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public string CsrfToken { get; set; } = string.Empty;

    public DateTime AccessExpiresAt { get; set; }

    public DateTime RefreshExpiresAt { get; set; }

    public UserResponse User { get; set; } = null!;
}