using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.Quiz;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuizCreateRequest request)
    {
        var result = await _quizService.CreateAsync(request.Title, request.Description, request.UserId);
        return StatusCode(result.StatusCode);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] QuizUpdateRequest request)
    {
        var result = await _quizService.UpdateAsync(id, request.Title, request.Description, request.UserId);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _quizService.DeleteAsync(id);
        return StatusCode(result.StatusCode, result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _quizService.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string title, string description, int? userId)
    {
        var result = await _quizService.GetAsync(title, description, userId);
        return StatusCode(result.StatusCode, result.Data);
    }
}