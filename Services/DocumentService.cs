using MorningIntegration.Interface;
using MorningIntegration.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace MorningIntegration.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ClientService> _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public DocumentService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger, IConfiguration config, IAccountService accountService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
            _accountService = accountService;
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            var token = await _accountService.GetToken("044577f7-6513-44ff-9537-7df4d77310c2", "eO5q9QcbDwOij73XN2z-3w");
         
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var json = JsonSerializer.Serialize(document);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to create a new document.");
                var response = await httpClient.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/documents", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to create document failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to create document failed with status code {response.StatusCode}. Response: {errorContent}");
                }

                var result = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Received JSON Response from API: {Result}", result);

                try
                {
                    var createdDocument = JsonSerializer.Deserialize<Document>(result);
                    _logger.LogDebug("Deserialized Document: {@Document}", createdDocument);
                    return createdDocument;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing JSON response to Document object.");
                    throw;
                }
            }

        }
    }
}
