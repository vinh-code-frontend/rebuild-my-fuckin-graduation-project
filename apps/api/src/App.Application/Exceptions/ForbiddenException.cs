namespace App.Application.Exceptions;

public sealed class ForbiddenException(string message, object? data = null)
    : AppException(message, StatusCodes.Status403Forbidden, data);
