using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Api.Models.User;
using QuizApplication.Application.Services;

namespace QuizApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _userService.AuthenticateAsync(request.Email, request.Password);
            if(result == null) return Unauthorized("Invalid email or password");
            return Ok(result);
        }
    }
}
