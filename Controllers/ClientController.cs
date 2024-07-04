using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MorningIntegration.Interface;
using MorningIntegration.Models;
using MorningIntegration.Services;

namespace MorningIntegration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("create-client")]
        public async Task<IActionResult> CreateClient(string id, string secret,[FromBody] Client client)
        {
            try
            {
                var createdClient = await _clientService.CreateClientAsync(client, id, secret);
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("update-client/{clientId}")]
        public async Task<IActionResult> UpdateClient(string clientId, string id, string secret,[FromBody] Client updatedClient)
        {
            if (string.IsNullOrEmpty(clientId) || updatedClient == null)
            {
                return BadRequest("Invalid client ID or client data.");
            }

            try
            {
                var updatedClientResponse = await _clientService.UpdateClientAsync(clientId, updatedClient, id, secret);
                return Ok(updatedClientResponse);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}