namespace QuizApplication.Api.Models.User;

public class UserUpdateRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}