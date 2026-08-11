namespace App.Application.Repositories;

public interface IRefreshTokenRepository
{
    void AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> FindRefreshToken(string hashedToken);
}
