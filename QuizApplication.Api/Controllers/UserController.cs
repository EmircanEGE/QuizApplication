using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.User;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        var result = await _userService.CreateAsync(request.FullName, request.Email, request.Password);
        return StatusCode(result.StatusCode, result.Data);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateRequest request)
    {
        var result = await _userService.UpdateAsync(id, request.FullName, request.Email, request.Password);
        return StatusCode(result.StatusCode);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _userService.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string fullName, string email)
    {
        var result = await _userService.GetAsync(fullName, email);
        return Ok(result);
    }
}