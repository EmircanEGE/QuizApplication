using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;

namespace QuizApplication.Application.Services;

public interface IAnswerService
{
    Task<ApiResponse<AnswerDto>> CreateAsync(string text, bool isCorrect, int questionId);
    Task<ApiResponse<AnswerDto>> UpdateAsync(int id, string text, bool isCorrect, int questionId);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<AnswerDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<AnswerDto>>> GetAsync(string text, bool? isCorrect, int? questionId);
}