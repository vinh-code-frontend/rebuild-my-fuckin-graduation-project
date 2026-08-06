namespace App.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest RegisterRequest);
    Task<LoginResponse> LoginAsync(LoginRequest LoginRequest);
    Task<LoginResponse> RefreshTokenAsync(string refreshToken);
}
