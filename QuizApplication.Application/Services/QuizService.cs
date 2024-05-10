using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;
using QuizApplication.Data;
using QuizApplication.Data.Models;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    public int LoggedInUserId;
    public readonly IHttpContextAccessor _HttpContextAccessor;

    public QuizService(IUserRepository userRepository, IQuizRepository quizRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _quizRepository = quizRepository;
        _unitOfWork = unitOfWork;
        _HttpContextAccessor = httpContextAccessor;
        LoggedInUserId = Convert.ToInt32(_HttpContextAccessor.HttpContext.User.FindFirstValue("userId"));
    }

    public async Task<ApiResponse<QuizDto>> CreateAsync(string title, string description, int userId)
    {
        var user = await _userRepository.GetAsync(x => x.Id == userId).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<QuizDto>(404, "User not found!");

        var quiz = new Quiz(LoggedInUserId, title, description, userId);
        await _quizRepository.InsertAsync(quiz);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuizDto>(201);
    }

    public async Task<ApiResponse<QuizDto>> UpdateAsync(int id, string title, string description, int userId)
    {
        var user = await _userRepository.GetAsync(x => x.Id == userId).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<QuizDto>(404, "User not found!");

        var quiz = await _quizRepository.GetAsync(x => x.Id == id).Include(x => x.User).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuizDto>(404, "Quiz not found!");

        quiz.Update(LoggedInUserId, title, description, userId, user);
        _quizRepository.Update(quiz);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<QuizDto>(204, QuizDto.Map(quiz));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<bool>(404, "Quiz not found!");
        _quizRepository.Delete(quiz);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool>(204);
    }

    public async Task<ApiResponse<QuizDto>> GetByIdAsync(int id)
    {
        var quiz = await _quizRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (quiz == null) return new ApiResponse<QuizDto>(404, "Quiz not found!");
        return new ApiResponse<QuizDto>(200, QuizDto.Map(quiz));
    }
    
    public async Task<ApiResponse<List<QuizDto>>> GetAsync(string title, string description, int? userId)
    {
        var quizzes = _quizRepository.GetAsync(x => true);
        if (!string.IsNullOrWhiteSpace(title))
            quizzes = quizzes.Where(x => x.Title == title);
        if (!string.IsNullOrWhiteSpace(description))
            quizzes = quizzes.Where(x => x.Description == description);
        if (userId != null)
            quizzes = quizzes.Where(x => x.UserId == userId);
        return new ApiResponse<List<QuizDto>>(200, quizzes.Select(x => QuizDto.Map(x)).ToList());
    }
}