namespace TaskFlow.Application.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }

    public static Result Success(int statusCode = 200)
        => new() { IsSuccess = true, StatusCode = statusCode };

    public static Result Failure(string error, int statusCode = 400)
        => new() { IsSuccess = false, Error = error, StatusCode = statusCode };

    public static Result<T> Success<T>(T data, int statusCode = 200)
        => new() { IsSuccess = true, Data = data, StatusCode = statusCode };

    public static Result<T> Failure<T>(string error, int statusCode = 400)
        => new() { IsSuccess = false, Error = error, StatusCode = statusCode };
}

public class Result<T> : Result
{
    public T? Data { get; set; }
}
