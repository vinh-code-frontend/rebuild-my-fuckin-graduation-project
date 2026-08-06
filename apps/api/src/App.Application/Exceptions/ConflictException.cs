namespace App.Application.Exceptions;

public sealed class ConflictException(string message, object? data = null)
    : AppException(message, StatusCodes.Status409Conflict, data);
