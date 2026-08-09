using App.Domain.Enums;

namespace App.Application.DTOs;

public class CreateUserRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
}
