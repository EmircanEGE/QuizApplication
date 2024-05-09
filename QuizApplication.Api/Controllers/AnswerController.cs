using System.Security.Claims;
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
        var createdBy = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await _answerService.CreateAsync(createdBy, request.Text, request.IsCorrect, request.QuestionId);
        return StatusCode(result.StatusCode);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AnswerUpdateRequest request)
    {
        var updatedBy = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await _answerService.UpdateAsync(updatedBy, id, request.Text, request.IsCorrect, request.QuestionId);
        if (result.StatusCode == 404) return StatusCode(result.StatusCode, result.Message);
        return StatusCode(result.StatusCode ,result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _answerService.DeleteAsync(id);
        return StatusCode(result.StatusCode, result.Message);
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
        return StatusCode(result.StatusCode, result.Data);
    }
}