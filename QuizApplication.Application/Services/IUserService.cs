using QuizApplication.Application.Dtos;

namespace QuizApplication.Application.Services;

public interface IUserService
{
    Task<UserDto> CreateAsync(string fullName, string email, string password);
    Task<List<UserDto>> GetAsync(string fullname, string email, string password);
}