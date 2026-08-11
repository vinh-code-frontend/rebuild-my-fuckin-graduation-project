namespace App.Application.Mappings;

public static class UserMapper
{
    public static UserResponse ToUserResponse(this User user)
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

