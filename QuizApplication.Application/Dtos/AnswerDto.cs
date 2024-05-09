using QuizApplication.Data.Models;

namespace QuizApplication.Application.Dtos;

public class AnswerDto : BaseDto
{
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
    public QuestionDto? Question { get; set; }

    public static AnswerDto Map(Answer answer)
    {
        return new AnswerDto
        {
            CreatedBy = answer.CreatedBy,
            UpdatedBy = answer.UpdatedBy,
            Id = answer.Id,
            CreatedTime = answer.CreatedTime,
            UpdatedTime = answer.UpdatedTime,
            Text = answer.Text,
            IsCorrect = answer.IsCorrect,
            QuestionId = answer.QuestionId,
            Question = answer.Question == null ? null : QuestionDto.Map(answer.Question)
        };
    }
}