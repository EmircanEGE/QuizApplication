using QuizApplication.Application.Dtos;

namespace QuizApplication.Application.Services;

public interface IUserService
{
    Task<UserDto> CreateAsync(string fullName, string email, string password);
    Task<UserDto> UpdateAsync(int id, string fullName, string email, string password);
    Task DeleteAsync(int id);
    Task<UserDto> GetByIdAsync(int id);
    Task<List<UserDto>> GetAsync(string fullname, string email, string password);
}