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

        [HttpPut("update-client/{id}")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody] Client updatedClient)
        {
            if (string.IsNullOrEmpty(id) || updatedClient == null)
            {
                return BadRequest("Invalid client ID or client data.");
            }

            try
            {
                var updatedClientResponse = await _clientService.UpdateClientAsync(id, updatedClient);
                return Ok(updatedClientResponse);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}