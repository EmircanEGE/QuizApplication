using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Core.Models;
using QuizApplication.Data;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork,
        IQuizRepository quizRepository)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
        _quizRepository = quizRepository;
    }

    public async Task<QuestionDto> CreateAsync(string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new QuestionDto();

        var question = new Question(text, quizId);
        await _questionRepository.InsertAsync(question);
        await _unitOfWork.SaveChangesAsync();
        var result = await _questionRepository.GetAsync(x => x.Id == question.Id).Include(x => x.Quiz)
            .FirstOrDefaultAsync();
        return QuestionDto.Map(result);
    }

    public async Task<QuestionDto> UpdateAsync(int id, string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new QuestionDto();

        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null) return new QuestionDto();

        question.Update(text, quizId);
        _questionRepository.Update(question);
        await _unitOfWork.SaveChangesAsync();
        return QuestionDto.Map(question);
    }

    public async Task DeleteAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        _questionRepository.Delete(question);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<QuestionDto> GetByIdAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null) return new QuestionDto();
        return QuestionDto.Map(question);
    }

    public async Task<List<QuestionDto>> GetAsync(string text, int? quizId)
    {
        var question = _questionRepository.GetAsync(x => true).Include(x => x.Quiz);
        if (!string.IsNullOrWhiteSpace(text))
            question = question.Where(x => x.Text == text).Include(x => x.Quiz);
        if (quizId != null)
            question = question.Where(x => x.QuizId == quizId).Include(x => x.Quiz);
        return question.Select(x => QuestionDto.Map(x)).ToList();
    }
}