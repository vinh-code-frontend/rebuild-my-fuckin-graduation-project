namespace App.Application.Exceptions;

public sealed class UnauthorizedException(string message, object? data = null)
    : AppException(message, StatusCodes.Status401Unauthorized, data);
