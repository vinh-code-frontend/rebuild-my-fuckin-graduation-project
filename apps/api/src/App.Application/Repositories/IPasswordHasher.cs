namespace App.Application.Repositories;

public interface IPasswordHasher
{
    string HashPassword(string plainPassword);
    bool Verify(string plainPassword, string HashedPassword);
}