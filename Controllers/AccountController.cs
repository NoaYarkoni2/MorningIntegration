using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
using MorningIntegration.Data;
using MorningIntegration.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using MorningIntegration.Interface;
using MorningIntegration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Net;


namespace MorningIntegration.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginUser user)
        //{
        //    var token = _accountService.Login(user.Email, user.Password);

        //    if (token == null)
        //        return BadRequest(new { message = "User name or password is incorrect" });

        //    return Ok(token);
        //}
        [HttpPost("getToken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                var token = await _accountService.GetToken();

                return Ok(new { Token = token });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
 

    }
}