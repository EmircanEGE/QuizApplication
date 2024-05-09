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
        if (result == null) return Unauthorized("Invalid email or password");
        return Ok(result.Token);
    }
}