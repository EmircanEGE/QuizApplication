namespace QuizApplication.Core.Models
{
    public class ApiResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
