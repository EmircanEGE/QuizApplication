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

    [HttpPut]
    public async Task<IActionResult> Update(int id, string fullName, string email, string password)
    {
        var result = _userService.UpdateAsync(id, fullName, email, password);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _userService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string fullName, string email, string password)
    {
        var result = await _userService.GetAsync(fullName, email, password);
        return Ok(result);
    }
}