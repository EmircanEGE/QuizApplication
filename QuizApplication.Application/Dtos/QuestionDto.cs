using QuizApplication.Core.Models;

namespace QuizApplication.Application.Dtos;

public class QuestionDto
{
    public string Text { get; set; }
    public int QuizId { get; set; }

    public static QuestionDto Map(Question question)
    {
        return new QuestionDto
        {
            QuizId = question.QuizId,
            Text = question.Text
        };
    }
}