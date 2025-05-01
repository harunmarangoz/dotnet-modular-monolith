namespace Shared.Domain.Exceptions;

public class AppValidationException(string message) : AppException(message)
{
    public List<string> Errors { get; set; } = new();
}