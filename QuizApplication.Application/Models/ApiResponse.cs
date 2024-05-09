namespace QuizApplication.Application.Models;

public class ApiResponse<T>
{
    public ApiResponse(int statusCode, string message, T? data = default)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public ApiResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public ApiResponse(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ApiResponse(int statusCode, T data)
    {
        StatusCode = statusCode;
        Data = data;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}