using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.Question;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuestionCreateRequest request)
    {
        var result = await _questionService.CreateAsync(request.Text, request.QuizId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] QuestionUpdateRequest request)
    {
        var result = await _questionService.UpdateAsync(id, request.Text, request.QuizId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _questionService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _questionService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string text, int? quizId)
    {
        var result = await _questionService.GetAsync(text, quizId);
        return Ok(result);
    }
}