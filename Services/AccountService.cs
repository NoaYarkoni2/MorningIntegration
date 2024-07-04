using System.Text.Json;
using MorningIntegration.Models;
using Microsoft.AspNetCore.Identity;
using MorningIntegration.Interface;


namespace MorningIntegration.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AccountService> _logger;



        public AccountService(UserManager<IdentityUser> userManager, IConfiguration config, IHttpClientFactory httpClientFactory, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<String> GetToken(string id, string secret)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            var values = new Dictionary<string, string>
        {
           { "id", id },
           { "secret", secret }
        };
            var json = JsonSerializer.Serialize(values);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");          
            _logger.LogInformation("Sending request to Green Invoice API.");    
            try
            {
                var response = await client.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/account/token", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to Green Invoice API failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to Green Invoice API failed with status code {response.StatusCode}. Response: {errorContent}");
                }

                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from Green Invoice API.");

                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(result);  
                return tokenResponse.token;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "HTTP request to Green Invoice API failed.");
                throw;
            }
        }
    }
}