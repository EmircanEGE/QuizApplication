using QuizApplication.Data.Models;

namespace QuizApplication.Application.Dtos;

public class QuizDto : BaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }

    public static QuizDto Map(Quiz quiz)
    {
        return new QuizDto
        {
            CreatedBy = quiz.CreatedBy,
            UpdatedBy = quiz.UpdatedBy,
            Id = quiz.Id,
            CreatedTime = quiz.CreatedTime,
            UpdatedTime = quiz.UpdatedTime,
            Title = quiz.Title,
            Description = quiz.Description,
            UserId = quiz.UserId
        };
    }
}