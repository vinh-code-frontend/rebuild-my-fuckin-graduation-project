using App.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using App.Application.Repositories;
using App.Application.Interfaces;

namespace App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, ITokenService tokenRepository) : ControllerBase
{
    private readonly string refreshTokenKey = "refresh-token";
    private readonly string csrfTokenKey = "csrf-token";
    private readonly string csrfHeaderKey = "X-CSRF-Token";

    [HttpPost("register")]
    public async Task<bool> Register([FromBody] RegisterRequest RegisterRequest)
    {
        return await authService.RegisterAsync(RegisterRequest);
    }

    [HttpPost("login")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest LoginRequest)
    {
        var result = await authService.LoginAsync(LoginRequest);
        AppendAuthCookies(result);

        return result;
    }

    [HttpPost("refresh")]
    public async Task<LoginResponse> Refresh()
    {
        var refreshToken = Request.Cookies[refreshTokenKey];
        var csrfToken = Request.Cookies[csrfTokenKey];
        var csrfHeader = Request.Headers[csrfHeaderKey];
        if (refreshToken is string && csrfToken is string && csrfToken == csrfHeader)
        {
            var result = await authService.RefreshTokenAsync(refreshToken);
            AppendAuthCookies(result);

            return result;
        }
        throw new Exception("Failed to refresh token");
    }

    private void AppendAuthCookies(LoginResponse response)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddDays(tokenRepository.GetExpiredRefreshTokenDays());

        Response.Cookies.Append(
            refreshTokenKey,
            response.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expiresAt
            });

        Response.Cookies.Append(
            csrfTokenKey,
            response.CsrfToken,
            new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expiresAt
            });
    }
}
