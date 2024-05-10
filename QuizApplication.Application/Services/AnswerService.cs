using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;
using QuizApplication.Data;
using QuizApplication.Data.Models;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public int LoggedInUserId;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository,
        IQuestionRepository questionRepository, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _httpContextAccessor = httpContextAccessor;
        LoggedInUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("userId"));
    }

    public async Task<ApiResponse<AnswerDto>> CreateAsync(string text, bool isCorrect, int questionId)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == questionId).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<AnswerDto>(404, "Question not found!");

        var answer = new Answer(LoggedInUserId, text, isCorrect, questionId);
        await _answerRepository.InsertAsync(answer);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<AnswerDto>(201);
    }

    public async Task<ApiResponse<AnswerDto>> UpdateAsync(int id, string text, bool isCorrect, int questionId)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == questionId).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<AnswerDto>(404, "Question not found!");

        var answer = await _answerRepository.GetAsync(x => x.Id == id).Include(x => x.Question).FirstOrDefaultAsync();
        if (answer == null) return new ApiResponse<AnswerDto>(404, "Answer not found!");

        answer.Update(LoggedInUserId, text, isCorrect, questionId, question);
        _answerRepository.Update(answer);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<AnswerDto>(200, AnswerDto.Map(answer));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var answer = await _answerRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (answer == null) return new ApiResponse<bool>(404, "Answer not found!");
        _answerRepository.Delete(answer);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool>(204);
    }

    public async Task<ApiResponse<AnswerDto>> GetByIdAsync(int id)
    {
        var answer = await _answerRepository.GetAsync(x => x.Id == id).Include(x => x.Question)
            .FirstOrDefaultAsync();
        if (answer == null) return new ApiResponse<AnswerDto>(404, "Answer not found!");
        return new ApiResponse<AnswerDto>(200, AnswerDto.Map(answer));
    }

    public async Task<ApiResponse<List<AnswerDto>>> GetAsync(string text, bool? isCorrect, int? questionId)
    {
        var answers = _answerRepository.GetAsync(x => true).Include(x => x.Question);
        if (!string.IsNullOrWhiteSpace(text))
            answers = answers.Where(x => x.Text == text).Include(x => x.Question);
        if (isCorrect != null)
            answers = answers.Where(x => x.IsCorrect == isCorrect).Include(x => x.Question);
        if (questionId != null)
            answers = answers.Where(x => x.QuestionId == questionId).Include(x => x.Question);
        return new ApiResponse<List<AnswerDto>>(200, answers.Select(x => AnswerDto.Map(x)).ToList());
    }
}