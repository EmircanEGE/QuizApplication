using QuizApplication.Data.Models;

namespace QuizApplication.Application.Dtos;

public class QuestionDto : BaseDto
{
    public string Text { get; set; }
    public int QuizId { get; set; }
    public QuizDto? Quiz { get; set; }

    public static QuestionDto Map(Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            CreatedTime = question.CreatedTime,
            UpdatedTime = question.UpdatedTime,
            QuizId = question.QuizId,
            Text = question.Text,
            Quiz = question.Quiz == null ? null : QuizDto.Map(question.Quiz)
        };
    }
}