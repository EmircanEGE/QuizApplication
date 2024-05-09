using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.User;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);
        if (result.StatusCode == 401) return StatusCode(result.StatusCode, result.Message);
        return StatusCode(result.StatusCode, result.Data.Token);
    }
}