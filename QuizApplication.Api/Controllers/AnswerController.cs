using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.Answer;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AnswerCreateRequest request)
    {
        var result = await _answerService.CreateAsync(request.Text, request.IsCorrect, request.QuestionId);
        return StatusCode(result.StatusCode, result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AnswerUpdateRequest request)
    {
        var result = await _answerService.UpdateAsync(id, request.Text, request.IsCorrect, request.QuestionId);
        return StatusCode(result.StatusCode);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _answerService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _answerService.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string text, bool? isCorrect, int? questionId)
    {
        var result = await _answerService.GetAsync(text, isCorrect, questionId);
        return Ok(result);
    }
}