using Microsoft.AspNetCore.Mvc;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string fullName, string email, string password)
    {
        var result = await _userService.CreateAsync(fullName, email, password);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string fullName, string email, string password)
    {
        var result = await _userService.GetAsync(fullName, email, password);
        return Ok(result);
    }
}