namespace App.Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbContext db) : IRefreshTokenRepository
{
    public void AddRefreshToken(RefreshToken refreshToken)
    {
        db.RefreshTokens.Add(refreshToken);
    }

    public async Task<RefreshToken?> FindRefreshToken(string hashedToken)
    {
        return await db.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.HashedToken == hashedToken);
    }
}
