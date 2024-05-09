using Microsoft.EntityFrameworkCore;
using QuizApplication.Api;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Models;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email && x.Password == password)
            .FirstOrDefaultAsync();
        if (user == null) return null;
        var userDto = UserDto.Map(user);
        var response = new LoginResponse();
        response.Token = _tokenService.GenerateToken(userDto);
        return response;
    }
}