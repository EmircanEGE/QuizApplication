using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;

namespace QuizApplication.Application.Services;

public interface IQuestionService
{
    Task<ApiResponse<QuestionDto>> CreateAsync(string text, int quizId);
    Task<ApiResponse<QuestionDto>> UpdateAsync(int id, string text, int quizId);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<QuestionDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<QuestionDto>>> GetAsync(string text, int? quizId, int page, int pageSize);
}