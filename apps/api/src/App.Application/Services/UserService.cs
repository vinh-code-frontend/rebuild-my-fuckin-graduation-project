using App.Api.Helpers;
using App.Domain.Enums;

namespace App.Application.Services;

public class UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();

        return users.Select(u => u.ToResponse()).ToList() ?? [];
    }

    public async Task<UserResponse?> GetUserByIdAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);

        return user?.ToResponse();
    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequestDto payload)
    {
        string username = payload.Username.Trim().ToLower();
        string email = payload.Email.Trim().ToLower();

        var (isUsernameExist, isEmailExist) = await userRepository.CheckExistAsync(username, email);

        if (isUsernameExist)
        {
            throw new Exception($"Username {payload.Username} is already existing! Try another value to process!");
        }
        if (isEmailExist)
        {
            throw new Exception($"Email {payload.Email} is already existing! Try another value to process!");
        }
        var tempPwd = PasswordGenerator.Generate();
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            Role = payload.Role,
            Status = UserStatus.Pending,
            HashedPassword = passwordHasher.HashPassword(tempPwd),
            CreatedAt = DateTime.UtcNow,

        };
        userRepository.AddUser(newUser);
        await unitOfWork.SaveChangesAsync();

        return new()
        {
            TempPassword = tempPwd,
            Id = newUser.Id,
            Username = newUser.Username,
            Email = newUser.Email,
            Role = newUser.Role,
            Status = newUser.Status,
            CreatedAt = newUser.CreatedAt
        };
    }
    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId) ?? throw new Exception("User not found");

        userRepository.DeleteUser(user);

        await unitOfWork.SaveChangesAsync();
    }
}
