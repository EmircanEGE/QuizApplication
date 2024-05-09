namespace QuizApplication.Api.Models.Question;

public class QuestionUpdateRequest
{
    public string Text { get; set; }
    public int QuizId { get; set; }
}