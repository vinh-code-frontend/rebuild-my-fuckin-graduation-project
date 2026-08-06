namespace App.Application.Repositories;

public interface IReFreshTokenRepository
{
    void AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> FindRefreshToken(string hashedToken);
}
