namespace Shared.Domain.Exceptions;

public class AppNotFoundException(string message) : AppException(message);