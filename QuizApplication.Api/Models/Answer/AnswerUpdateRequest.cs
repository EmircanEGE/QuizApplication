namespace QuizApplication.Api.Models.Answer;

public class AnswerUpdateRequest
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}