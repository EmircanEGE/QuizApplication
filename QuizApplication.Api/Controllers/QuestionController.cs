using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Create(string text, int quizId)
    {
        var result = await _questionService.CreateAsync(text, quizId);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, string text, int quizId)
    {
        var result = await _questionService.UpdateAsync(id, text, quizId);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        _questionService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _questionService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string text, int? quizId)
    {
        var result = await _questionService.GetAsync(text, quizId);
        return Ok(result);
    }
}