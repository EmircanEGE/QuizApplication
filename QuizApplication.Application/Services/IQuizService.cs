using QuizApplication.Application.Dtos;

namespace QuizApplication.Application.Services;

public interface IQuizService
{
    Task<QuizDto> CreateAsync(string title, string description, int createdBy);
    Task<QuizDto> UpdateAsync(int id, string title, string description, int createdBy);
    Task DeleteAsync(int id);
    Task<QuizDto> GetByIdAsync(int id);
    Task<List<QuizDto>> GetAsync(string title, string description, int? createdBy);
}