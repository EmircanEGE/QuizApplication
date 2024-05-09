namespace QuizApplication.Api.Models.Quiz;

public class QuizCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
}