using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Core.Models;
using QuizApplication.Data;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository,
        IQuestionRepository questionRepository)
    {
        _unitOfWork = unitOfWork;
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
    }

    public async Task<AnswerDto> CreateAsync(string text, bool isCorrect, int questionId)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == questionId).FirstOrDefaultAsync();
        if (question == null) return new AnswerDto();

        var answer = new Answer(text, isCorrect, questionId);
        await _answerRepository.InsertAsync(answer);
        await _unitOfWork.SaveChangesAsync();
        var result = await _answerRepository.GetAsync(x => x.Id == answer.Id).Include(x => x.Question)
            .FirstOrDefaultAsync();
        return AnswerDto.Map(result);
    }

    public async Task<AnswerDto> UpdateAsync(int id, string text, bool isCorrect, int questionId)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == questionId).FirstOrDefaultAsync();
        if (question == null) return new AnswerDto();

        var answer = await _answerRepository.GetAsync(x => x.Id == id).Include(x => x.Question).FirstOrDefaultAsync();
        if (answer == null) return new AnswerDto();

        answer.Update(text, isCorrect, questionId);
        _answerRepository.Update(answer);
        await _unitOfWork.SaveChangesAsync();
        return AnswerDto.Map(answer);
    }

    public async Task DeleteAsync(int id)
    {
        var answer = await _answerRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (answer == null) return;

        _answerRepository.Delete(answer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<AnswerDto> GetByIdAsync(int id)
    {
        var answer = await _answerRepository.GetAsync(x => x.Id == id).Include(x => x.Question)
            .FirstOrDefaultAsync();
        if (answer == null) return new AnswerDto();
        return AnswerDto.Map(answer);
    }

    public async Task<List<AnswerDto>> GetAsync(string text, bool? isCorrect, int? questionId)
    {
        var answers = _answerRepository.GetAsync(x => true).Include(x => x.Question);
        if (!string.IsNullOrWhiteSpace(text))
            answers = answers.Where(x => x.Text == text).Include(x => x.Question);
        if (isCorrect != null)
            answers = answers.Where(x => x.IsCorrect == isCorrect).Include(x => x.Question);
        if (questionId != null)
            answers = answers.Where(x => x.QuestionId == questionId).Include(x => x.Question);
        return answers.Select(x => AnswerDto.Map(x)).ToList();
    }
}