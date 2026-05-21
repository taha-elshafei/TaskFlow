namespace TaskFlow.Application.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public static Result Success(int statusCode = 200)
        => new() { IsSuccess = true, StatusCode = statusCode };

    public static Result Failure(string message, int statusCode = 400)
        => new() { IsSuccess = false, Message = message, StatusCode = statusCode };

    public static Result<T> Success<T>(T data, int statusCode = 200)
    {
        return new() { IsSuccess = true, Data = data, StatusCode = statusCode };
    }

    public static Result<T> Failure<T>(string message, int statusCode = 400)
        => new() { IsSuccess = false, Message = message, StatusCode = statusCode };
}

public class Result<T> : Result
{
    public T? Data { get; set; }
}
