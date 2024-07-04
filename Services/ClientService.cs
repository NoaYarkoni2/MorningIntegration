using MorningIntegration.Interface;
using MorningIntegration.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

        public async Task<Client> CreateClientAsync(Client client, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);        
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var json = JsonSerializer.Serialize(client);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to create a new client.");
                var response = await httpClient.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/clients", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to create client failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to create client failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from creating client.");
                var newClient= JsonSerializer.Deserialize<Client>(result);
                client.id = newClient.id;
                return newClient;
            }
        }

        public async Task<Client> UpdateClientAsync(string clientId, Client updatedClient, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var json = JsonSerializer.Serialize(updatedClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to create a new client.");
                var response = await httpClient.PutAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/clients/{clientId}", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to create client failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to create client failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from updating client.");
                return JsonSerializer.Deserialize<Client>(result);
            }

        }
    }
}





