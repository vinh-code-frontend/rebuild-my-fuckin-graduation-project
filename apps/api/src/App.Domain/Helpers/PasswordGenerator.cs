using System.Security.Cryptography;
namespace App.Api.Helpers;

public static class PasswordGenerator
{
    private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%";

    public static string Generate(int length = 12)
    {
        return string.Concat(
            Enumerable.Range(0, length)
                .Select(_ => Chars[RandomNumberGenerator.GetInt32(Chars.Length)])
        );
    }
}