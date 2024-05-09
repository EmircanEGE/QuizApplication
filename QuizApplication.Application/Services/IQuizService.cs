using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;

namespace QuizApplication.Application.Services;

public interface IQuizService
{
    Task<ApiResponse<QuizDto>> CreateAsync(string title, string description, int userId);
    Task<ApiResponse<QuizDto>> UpdateAsync(int id, string title, string description, int userId);
    Task DeleteAsync(int id);
    Task<ApiResponse<QuizDto>> GetByIdAsync(int id);
    Task<List<QuizDto>> GetAsync(string title, string description, int? userId);
}