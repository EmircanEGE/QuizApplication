using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
    public int LoggedInUserId;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork,
        IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
        LoggedInUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("userId"));
    }

    public async Task<ApiResponse<QuestionDto>> CreateAsync(string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuestionDto>(404, "Quiz not found!");

        var question = new Question(LoggedInUserId, text, quizId);
        await _questionRepository.InsertAsync(question);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuestionDto>(201);
    }

    public async Task<ApiResponse<QuestionDto>> UpdateAsync(int id, string text, int quizId)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == quizId).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuestionDto>(404, "Quiz not found!");

        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<QuestionDto>(404, "Question not found!");

        question.Update(LoggedInUserId, text, quizId, quiz);
        _questionRepository.Update(question);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuestionDto>(200, QuestionDto.Map(question));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (question == null) return new ApiResponse<bool>(404, "Question not found!");
        _questionRepository.Delete(question);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool>(204);
    }

    public async Task<ApiResponse<QuestionDto>> GetByIdAsync(int id)
    {
        var question = await _questionRepository.GetAsync(x => x.Id == id).Include(x => x.Quiz).FirstOrDefaultAsync();
        if (question == null)
            return new ApiResponse<QuestionDto>(404, "Question not found!");
        return new ApiResponse<QuestionDto>(200);
    }

    public async Task<ApiResponse<List<QuestionDto>>> GetAsync(string text, int? quizId, int page, int pageSize)
    {
        if (page == 0 || pageSize == 0)
            return new ApiResponse<List<QuestionDto>>(400, "Page or page size must be greater than 0");
        var question = _questionRepository.GetAsync(x => true).Include(x => x.Quiz);
        if (!string.IsNullOrWhiteSpace(text))
            question = question.Where(x => x.Text == text).Include(x => x.Quiz);
        if (quizId != null)
            question = question.Where(x => x.QuizId == quizId).Include(x => x.Quiz);
        var questionDto = question.Select(x => QuestionDto.Map(x)).ToList();

        var totalCount = questionDto.Count;
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        if (page > totalPages) return new ApiResponse<List<QuestionDto>>(404, "Page not found!");
        var questionPerPage = questionDto.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new ApiResponse<List<QuestionDto>>(200, questionPerPage);
        
    }
}