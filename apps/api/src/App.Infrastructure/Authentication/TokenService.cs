using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Authentication;

public class TokenService : ITokenService
{
    private readonly JwtSettings jwtSettings;
    public TokenService(IConfiguration configuration)
    {
        jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()!;
    }
    public string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
    public string GenerateCstfToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes);
    }
    public int GetExpiredRefreshTokenDays()
    {
        return jwtSettings.RefreshTokenDays;
    }
    public (string token, DateTime expired) GenerateAccessToken(User user)
    {
        DateTime expiry = DateTime.UtcNow.AddMinutes(jwtSettings.ExpireMinutes);

        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString()),
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
    }
    public (RefreshToken entity, string plainToken) GenerateRefreshToken(Guid userId)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var newRefreshToken = new RefreshToken()
        {
            Id = new Guid(),
            UserId = userId,
            HashedToken = HashToken(refreshToken),
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenDays)
        };
        return (newRefreshToken, refreshToken);
    }
}
