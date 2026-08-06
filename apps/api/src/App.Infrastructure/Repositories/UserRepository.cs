namespace App.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await db.Users.FindAsync(userId);
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        string normalizedUsername = username.Trim().ToLower();

        return await db.Users.FirstOrDefaultAsync(user => user.Username.Trim().ToLower() == normalizedUsername);
    }
    public void AddUser(User user)
    {
        db.Users.Add(user);
    }
    public void DeleteUser(User user)
    {
        db.Users.Remove(user);
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await db.Users.ToListAsync();
    }
    public async Task<(bool isUsernameExist, bool isEmailExist)> CheckExistAsync(string username, string email)
    {
        bool isUsernameExist = await db.Users.AnyAsync(u => u.Username == username);
        bool isEmailExist = await db.Users.AnyAsync(u => u.Email == email);

        return (isUsernameExist, isEmailExist);
    }
}
