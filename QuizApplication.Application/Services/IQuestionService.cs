using QuizApplication.Application.Dtos;

namespace QuizApplication.Application.Services;

public interface IQuestionService
{
    Task<QuestionDto> CreateAsync(string text, int quizId);
    Task<QuestionDto> UpdateAsync(int id, string text, int quizId);
    Task DeleteAsync(int id);
    Task<QuestionDto> GetByIdAsync(int id);
    Task<List<QuestionDto>> GetAsync(string text, int? quizId);
}