namespace Shared.Application.Results;

public class ListDataResult<T> : Result
{
    public List<T> Data { get; set; }

    public static ListDataResult<T> Success(List<T> data)
    {
        return new ListDataResult<T>
        {
            Data = data,
            HasError = false
        };
    }

    public new static ListDataResult<T> Failure(string message = null, List<KeyValuePair<string, string>> errors = null)
    {
        return new ListDataResult<T>
        {
            HasError = true,
            Message = message,
            Errors = errors
        };
    }
}