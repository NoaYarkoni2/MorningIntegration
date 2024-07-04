using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Models;
using MorningIntegration.Interface;
using Microsoft.AspNetCore.Authorization;


namespace MorningIntegration.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("getToken")]
        public async Task<IActionResult> GetToken([FromBody] TokenRequest request)
        {
            try
            {
                var token = await _accountService.GetToken(request.Id, request.Secret);

                return Ok(new { Token = token });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}