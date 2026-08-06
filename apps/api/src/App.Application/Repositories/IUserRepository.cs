namespace App.Application.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<(bool isUsernameExist, bool isEmailExist)> CheckExistAsync(string username, string email);
    void AddUser(User user);
    void DeleteUser(User user);
}