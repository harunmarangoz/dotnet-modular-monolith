namespace Shared.Application.Results;

public class DataResult<T> : Result
{
    public T Data { get; set; }

    public static DataResult<T> Success(T data, string message = null)
    {
        return new DataResult<T>
        {
            HasError = false,
            Message = message,
            Data = data,
            Errors = null
        };
    }

    public new static DataResult<T> Failure(string message, List<KeyValuePair<string, string>> errors = null)
    {
        return new DataResult<T>
        {
            HasError = true,
            Message = message,
            Data = default,
            Errors = errors
        };
    }
}