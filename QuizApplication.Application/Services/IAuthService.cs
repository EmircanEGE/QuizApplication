using QuizApplication.Application.Models;

namespace QuizApplication.Application.Services;

public interface IAuthService
{
    Task<LoginResponse> AuthenticateAsync(string email, string password);
}