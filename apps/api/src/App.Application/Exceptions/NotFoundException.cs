namespace App.Application.Exceptions;

public sealed class NotFoundException(string message, object? data = null)
    : AppException(message, StatusCodes.Status404NotFound, data);
