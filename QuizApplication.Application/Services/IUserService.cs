using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;

namespace QuizApplication.Application.Services;

public interface IUserService
{
    Task<ApiResponse<UserDto>> CreateAsync(string fullName, string email, string password);
    Task<ApiResponse<UserDto>> UpdateAsync(int id, string fullName, string email, string password);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<UserDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<UserDto>>> GetAsync(string fullname, string email, int page, int pageSize);
}