namespace App.Application.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse?> GetUserByIdAsync(Guid userId);
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequestDto payload);
    Task DeleteUserAsync(Guid userId);
}
