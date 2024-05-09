using QuizApplication.Application.Dtos;

namespace QuizApplication.Api;

public interface ITokenService
{
    string GenerateToken(UserDto user);
}