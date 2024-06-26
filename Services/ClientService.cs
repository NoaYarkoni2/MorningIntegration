using Microsoft.EntityFrameworkCore;
using MorningIntegration.Interface;
using MorningIntegration.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Data;
using MorningIntegration.Data;

namespace MorningIntegration.Services
{
    public class ClientService : IClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ClientService> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public ClientService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger, IConfiguration config, IAccountService accountService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
            _accountService = accountService;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            var token = await _accountService.GetToken("044577f7-6513-44ff-9537-7df4d77310c2", "eO5q9QcbDwOij73XN2z-3w");
            client.id = Guid.NewGuid().ToString();


            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                //var apiUrl = _config.GetValue<string>("ApiSettings:ClientEndpoint");
                var json = JsonSerializer.Serialize(client);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to create a new client.");
                var response = await httpClient.PostAsync("https://sandbox.d.greeninvoice.co.il/api/v1/clients", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to create client failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to create client failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from creating client.");
                return JsonSerializer.Deserialize<Client>(result);
            }
        }
    }
}





