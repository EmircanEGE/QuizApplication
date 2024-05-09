using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;
using QuizApplication.Data;
using QuizApplication.Data.Models;
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

    public async Task<ApiResponse<QuestionDto>> CreateAsync(string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuestionDto>(404, $"Quiz id = {quizId} not found!", new QuestionDto());

        var question = new Question(text, quizId);
        await _questionRepository.InsertAsync(question);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuestionDto>(201, "Question created successfully.", QuestionDto.Map(question));
    }

    public async Task<ApiResponse<QuestionDto>> UpdateAsync(int id, string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuestionDto>(404, $"Quiz id = {quizId} not found!", new QuestionDto());

        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<QuestionDto>(404, $"Question id = {id} not found!", new QuestionDto());

        question.Update(text, quizId, quiz);
        _questionRepository.Update(question);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuestionDto>(204, "Question updated successfully.", QuestionDto.Map(question));
    }

    public async Task DeleteAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        _questionRepository.Delete(question);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ApiResponse<QuestionDto>> GetByIdAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<QuestionDto>(404, "Question id = {id} not found!", new QuestionDto());
        return new ApiResponse<QuestionDto>(200, "", QuestionDto.Map(question));
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