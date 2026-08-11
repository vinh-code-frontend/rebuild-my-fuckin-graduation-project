namespace App.Application.Services;

public class AuthService(
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository,
    IRefreshTokenRepository reFreshTokenRepository,
    IUnitOfWork unitOfWork
    ) : IAuthService
{
    public async Task<bool> RegisterAsync(RegisterRequest RegisterRequest)
    {
        var newUser = new User
        {
            Username = RegisterRequest.Username.Trim().ToLower(),
            Email = RegisterRequest.Email.Trim().ToLower(),
            HashedPassword = passwordHasher.HashPassword(RegisterRequest.Password),
            CreatedAt = DateTime.UtcNow
        };

        userRepository.AddUser(newUser);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
    public async Task<LoginResponse> LoginAsync(LoginRequest LoginRequest)
    {
        var user = await userRepository.GetUserByUsernameAsync(LoginRequest.Username);

        if (user == null)
        {
            throw new Exception("Wrong username or password");
        }
        bool isValidPassword = passwordHasher.Verify(LoginRequest.Password, user.HashedPassword);
        if (!isValidPassword)
        {
            throw new Exception("Wrong username or password");
        }

        var (accessToken, accessTokenExpiredAt) = tokenService.GenerateAccessToken(user);
        var (refreshToken, plainRefreshToken) = tokenService.GenerateRefreshToken(user.Id);
        string csrfToken = tokenService.GenerateCstfToken();

        reFreshTokenRepository.AddRefreshToken(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return CreateLoginResponse(
            user,
            accessToken,
            accessTokenExpiredAt,
            plainRefreshToken,
            csrfToken,
            refreshToken.ExpiredAt);
    }
    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
    {
        string hashedToken = tokenService.HashToken(refreshToken);

        var session = await reFreshTokenRepository.FindRefreshToken(hashedToken);
        var user = session?.User;
        if (session == null || user == null)
        {
            throw new Exception("Invalid refresh token");
        }

        if (session.ExpiredAt <= DateTime.UtcNow)
        {
            throw new Exception("Refresh token expired");
        }

        if (session.RevokedAt != null)
        {
            throw new Exception("Refresh token reuse detected");
        }
        session.RevokedAt = DateTime.UtcNow;


        var (accessToken, accessTokenExpiredAt) = tokenService.GenerateAccessToken(user);
        var (newRefreshToken, plainRefreshToken) = tokenService.GenerateRefreshToken(session.UserId);
        string csrfToken = tokenService.GenerateCstfToken();

        reFreshTokenRepository.AddRefreshToken(newRefreshToken);
        await unitOfWork.SaveChangesAsync();

        return CreateLoginResponse(
            user,
            accessToken,
            accessTokenExpiredAt,
            plainRefreshToken,
            csrfToken,
            newRefreshToken.ExpiredAt);
    }

    private static LoginResponse CreateLoginResponse(
        User user,
        string accessToken,
        DateTime accessTokenExpiredAt,
        string refreshToken,
        string csrfToken,
        DateTime refreshTokenExpiredAt)
    {
        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            CsrfToken = csrfToken,
            AccessExpiresAt = accessTokenExpiredAt,
            RefreshExpiresAt = refreshTokenExpiredAt,
            User = user.ToResponse()
        };
    }
}