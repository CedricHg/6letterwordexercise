public class Result<T> : Result
    where T : class? // this is an iffy constraint, but need to support nullable types somehow
{
    public T? Data { get; init; }

    public Result(bool success, T? data, string? errorMessage = null)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T data)
    {
        return new Result<T>(true, data, null);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(false, null, errorMessage);
    }
}

public class Result
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }

    public static Result<T> Ok<T>(T data)
        where T : class?
    {
        return new Result<T>(true, data, null);
    }
}