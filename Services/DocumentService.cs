﻿using MorningIntegration.Interface;
using MorningIntegration.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
//using System.Reflection.Metadata;


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

        public async Task<Document> CreateDocumentAsync(Document document, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);         
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

        public async Task<Document> GetDocumentAsync(string documentId, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _logger.LogInformation("Sending Get request to retrieve a document with ID.");
                var response = await httpClient.GetAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/documents/{documentId}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to Get Document failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to Get Document failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from Get Document.");
                return JsonSerializer.Deserialize<Document>(result);
            }
        }

        public async Task<Document> CloseDocumentAsync(string documentId, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to close a document.");
                var response = await httpClient.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/documents/{documentId}/close", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to close document failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to close document failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Received JSON Response from API: {Result}", result);
                try
                {
                    var responseData = JsonSerializer.Deserialize<Document>(result);
                    _logger.LogDebug("Deserialized Document: {@Document}", responseData);
                    return responseData;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing JSON response to Document object.");
                    throw;
                }
            }
        }


        public async Task<GetPreviewResponse> GetPreviewDocumentAsync(string documentId, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var document = await GetDocumentAsync(documentId, id, secret);
                var json = JsonSerializer.Serialize(document);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to get a preview document.");
                var response = await httpClient.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/documents/preview", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to get a preview document failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to get a preview document failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Received JSON Response from API: {Result}", result);
                try
                {
                    var responseData = new GetPreviewResponse { File = result };
                    _logger.LogDebug("Deserialized Document: {@File}", responseData.File);
                    return responseData;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing JSON response to Document object.");
                    throw;
                }
            }
        }

        public async Task<DocumentSearchResponse> SearchDocumentsAsync(DocumentSearchRequest searchRequest, string id, string secret)
        {
            var token = await _accountService.GetToken(id, secret);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var json = JsonSerializer.Serialize(searchRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Sending POST request to search for documents.");
                var response = await httpClient.PostAsync($"{_config.GetValue<string>("ApiSettings:BaseUrl")}/documents/search", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Request to search for documents failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"Request to search for documents failed with status code {response.StatusCode}. Response: {errorContent}");
                }
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Received JSON Response from API: {Result}", result);
                try
                {
                    var responseData = JsonSerializer.Deserialize<DocumentSearchResponse>(result);
                    _logger.LogDebug("Deserialized Document: {@DocumentSearchResponse}", responseData);
                    _logger.LogInformation("Received DocumentSearchResponse: {@DocumentSearchResponse}", responseData);
                    return responseData;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing JSON response to DocumentSearchResponse object.");
                    throw;
                }
            }
        }

    }
}
