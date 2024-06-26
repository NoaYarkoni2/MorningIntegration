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
        //private readonly HttpClient _httpClient;
        //private readonly IConfiguration _config;
        //private readonly IAccountService _accountService;
        //private readonly DataContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ClientService> _logger;
        private readonly IConfiguration _config;
   
        public ClientService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
           
        }

            public async Task<Client> CreateClientAsync(Client client)
        {
            //var token = await _accountService.GetToken();
            client.Id = Guid.NewGuid().ToString();
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var apiUrl = _config.GetValue<string>("ApiSettings:ClientEndpoint");
            var json = JsonSerializer.Serialize(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            _logger.LogInformation("Sending POST request to create a new client.");
            var response = await httpClient.PostAsync("https://sandbox.d.greeninvoice.co.il/api/v1/clients" , content);
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





