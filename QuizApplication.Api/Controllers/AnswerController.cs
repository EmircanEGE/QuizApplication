using Microsoft.AspNetCore.Mvc;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string text, bool isCorrect, int questionId)
    {
        var result = await _answerService.CreateAsync(text, isCorrect, questionId);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string text, bool isCorrect, int questionId)
    {
        var result = await _answerService.UpdateAsync(id, text, isCorrect, questionId);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _answerService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _answerService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string text, bool? isCorrect, int? questionId)
    {
        var result = await _answerService.GetAsync(text, isCorrect, questionId);
        return Ok(result);
    }
}