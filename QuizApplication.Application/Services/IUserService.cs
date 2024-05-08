using QuizApplication.Application.Dtos;
using QuizApplication.Core.Models;

namespace QuizApplication.Application.Services;

public interface IUserService
{
    Task<ApiResponse<UserDto>> CreateAsync(string fullName, string email, string password);
    Task<ApiResponse<UserDto>> UpdateAsync(int id, string fullName, string email, string password);
    Task DeleteAsync(int id);
    Task<ApiResponse<UserDto>> GetByIdAsync(int id);
    Task<List<UserDto>> GetAsync(string fullname, string email);
    Task<LoginResponse> AuthenticateAsync(string email, string password);
}