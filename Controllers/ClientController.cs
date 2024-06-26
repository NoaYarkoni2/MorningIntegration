using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Interface;
using MorningIntegration.Models;
using MorningIntegration.Services;
using static MorningIntegration.Controllers.AccountController;

namespace MorningIntegration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        //[HttpPost("create-client")]
        //public async Task<IActionResult> CreateClient(Client client)
        //{
        //    var result = await _clientService.CreateClientAsync(client);
        //    return Ok(result);
        //}




        [HttpPost("create-client")]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            try
            {
                var createdClient = await _clientService.CreateClientAsync(client);
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}