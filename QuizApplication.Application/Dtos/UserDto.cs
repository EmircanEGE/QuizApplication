using QuizApplication.Data.Models;

namespace QuizApplication.Application.Dtos;

public class UserDto : BaseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static UserDto Map(User user)
    {
        return new UserDto
        {
            CreatedBy = user.CreatedBy,
            UpdatedBy = user.UpdatedBy,
            Id = user.Id,
            CreatedTime = user.CreatedTime,
            UpdatedTime = user.UpdatedTime,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password
        };
    }
}