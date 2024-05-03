using QuizApplication.Core.Models;

namespace QuizApplication.Application.Dtos;

public class QuizDto : BaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }

    public static QuizDto Map(Quiz quiz)
    {
        return new QuizDto
        {
            Title = quiz.Title,
            Description = quiz.Description,
            CreatedBy = quiz.CreatedBy
        };
    }
}