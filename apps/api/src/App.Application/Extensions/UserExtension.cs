using App.Domain.DTOs;
using App.Domain.Entities;

namespace App.Application.Extensions;

public static class UserExtension
{
    public static UserResponse ToResponse(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserResponse()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Status = user.Status,
            Role = user.Role,
        };
    }
}

