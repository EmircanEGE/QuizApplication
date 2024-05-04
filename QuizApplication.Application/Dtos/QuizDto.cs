using QuizApplication.Core.Models;

namespace QuizApplication.Application.Dtos;

public class QuizDto : BaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public UserDto? User { get; set; }

    public static QuizDto Map(Quiz quiz)
    {
        return new QuizDto
        {
            Id = quiz.Id,
            CreatedTime = quiz.CreatedTime,
            UpdatedTime = quiz.UpdatedTime,
            Title = quiz.Title,
            Description = quiz.Description,
            CreatedBy = quiz.CreatedBy,
            User = quiz.User == null ? null : UserDto.Map(quiz.User)
        };
    }
}