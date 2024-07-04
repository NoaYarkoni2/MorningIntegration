using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Models;
using MorningIntegration.Services;
using MorningIntegration.Interface;

namespace MorningIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var token = await _userService.AuthenticateAsync(user);

            if (token == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(new { Token = token });
        }
    }
}
