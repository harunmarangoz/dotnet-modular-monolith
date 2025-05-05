namespace Shared.Application.Results;

public class Result
{
    public bool HasError { get; set; }
    public string Message { get; set; }
    public List<KeyValuePair<string, string>> Errors { get; set; }

    public static Result Success(string message = null)
    {
        return new Result
        {
            HasError = false,
            Message = message,
            Errors = null
        };
    }
    
    public static Result Failure(string message = null, List<KeyValuePair<string, string>> errors = null)
    {
        return new Result
        {
            HasError = true,
            Message = message,
            Errors = errors
        };
    }
}