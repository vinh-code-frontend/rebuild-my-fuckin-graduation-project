namespace App.Application.Repositories;

public interface ITokenService
{
    string HashToken(string token);
    string GenerateCstfToken();
    int GetExpiredRefreshTokenDays();
    (string token, DateTime expired) GenerateAccessToken(User user);
    (RefreshToken entity, string plainToken) GenerateRefreshToken(Guid userId);
}