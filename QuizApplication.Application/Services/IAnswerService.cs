using QuizApplication.Application.Dtos;

namespace QuizApplication.Application.Services;

public interface IAnswerService
{
    Task<AnswerDto> CreateAsync(string text, bool isCorrect, int questionId);
    Task<AnswerDto> UpdateAsync(int id, string text, bool isCorrect, int questionId);
    Task DeleteAsync(int id);
    Task<AnswerDto> GetByIdAsync(int id);
    Task<List<AnswerDto>> GetAsync(string text, bool? isCorrect, int? questionId);
}