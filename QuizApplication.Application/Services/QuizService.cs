using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Core.Models;
using QuizApplication.Data;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public QuizService(IUserRepository userRepository, IQuizRepository quizRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _quizRepository = quizRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<QuizDto> CreateAsync(string title, string description, int createdBy)
    {
        var user = await _userRepository.GetAsync(x => x.Id == createdBy).FirstOrDefaultAsync();
        if (user == null) return new QuizDto();

        var quiz = new Quiz(title, description, createdBy);
        await _quizRepository.InsertAsync(quiz);
        await _unitOfWork.SaveChangesAsync();
        var result = await _quizRepository.GetAsync(x => x.Id == quiz.Id).Include(x => x.User)
            .FirstOrDefaultAsync();
        return QuizDto.Map(result);
    }

    public async Task<QuizDto> UpdateAsync(int id, string title, string description, int createdBy)
    {
        var user = await _userRepository.GetAsync(x => x.Id == createdBy).FirstOrDefaultAsync();
        if (user == null) return new QuizDto();

        var quiz = await _quizRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (quiz == null) return new QuizDto();

        quiz = new Quiz(title, description, createdBy);
        _quizRepository.Update(quiz);
        await _unitOfWork.SaveChangesAsync();
        return QuizDto.Map(quiz);
    }

    public async Task Delete(int id)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (quiz == null) return;

        _quizRepository.Delete(quiz);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<QuizDto> GetByIdAsync(int id)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == id).Include(x => x.User).FirstOrDefaultAsync();
        if (quiz == null) return new QuizDto();
        return QuizDto.Map(quiz);
    }

    public async Task<List<QuizDto>> GetAsync(string title, string description, int? createdBy)
    {
        var quizzes = _quizRepository.GetAsync(x => true).Include(x => x.User);
        if (!string.IsNullOrWhiteSpace(title))
            quizzes = quizzes.Where(x => x.Title == title).Include(x => x.User);
        if (!string.IsNullOrWhiteSpace(description))
            quizzes = quizzes.Where(x => x.Description == description).Include(x => x.User);
        if (createdBy != null)
            quizzes = quizzes.Where(x => x.CreatedBy == createdBy).Include(x => x.User);
        return quizzes.Select(x => QuizDto.Map(x)).ToList();
    }
}