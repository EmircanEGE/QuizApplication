using QuizApplication.Core.Models;

namespace QuizApplication.Application.Dtos;

public class AnswerDto : BaseDto
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }

    public static AnswerDto Map(Answer answer)
    {
        return new AnswerDto
        {
            Id = answer.Id,
            CreatedTime = answer.CreatedTime,
            UpdatedTime = answer.UpdatedTime,
            Text = answer.Text,
            IsCorrect = answer.IsCorrect,
            QuestionId = answer.QuestionId
        };
    }
}