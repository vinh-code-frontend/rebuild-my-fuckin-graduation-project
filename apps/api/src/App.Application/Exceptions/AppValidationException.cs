namespace App.Application.Exceptions;

public sealed class AppValidationException(string message, object? data = null)
    : AppException(message, StatusCodes.Status422UnprocessableEntity, data);
