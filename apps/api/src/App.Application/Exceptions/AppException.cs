namespace App.Application.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string message, int statusCode, object? data = null)
        : base(message)
    {
        StatusCode = statusCode;
        DataPayload = data;
    }

    public int StatusCode { get; }
    public object? DataPayload { get; }
}
