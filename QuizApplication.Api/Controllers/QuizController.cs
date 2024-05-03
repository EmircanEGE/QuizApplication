using Microsoft.AspNetCore.Mvc;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string title, string description, int createdBy)
    {
        var result = await _quizService.CreateAsync(title, description, createdBy);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string title, string description, int createdBy)
    {
        var result = await _quizService.UpdateAsync(id, title, description, createdBy);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        _quizService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _quizService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string title, string description, int? createdBy)
    {
        var result = await _quizService.GetAsync(title, description, createdBy);
        return Ok(result);
    }
}