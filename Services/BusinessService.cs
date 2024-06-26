//using Microsoft.AspNetCore.Identity;
//using MorningIntegration.Data;
//using System.Net.Http;
//using static MorningIntegration.Services.AccountService;
//using System.Text.Json;

//namespace MorningIntegration.Services
//{
//    public class BusinessService
//    {
//        private IHttpClientFactory _httpClientFactory;

//        public BusinessService(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<String> GetBusiness()
//        {
//            HttpClient client = _httpClientFactory.CreateClient();
//            var json = JsonSerializer.Serialize();
//            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
//            try
//            {
//                var response = await client.PostAsync("https://sandbox.d.greeninvoice.co.il/api/v1/businesses", content);

//                if (!response.IsSuccessStatusCode)
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    _logger.LogError("Request to Green Invoice API failed with status code {StatusCode}. Response: {Response}", response.StatusCode, errorContent);
//                    throw new HttpRequestException($"Request to Green Invoice API failed with status code {response.StatusCode}. Response: {errorContent}");
//                }

//                var result = await response.Content.ReadAsStringAsync();
//                _logger.LogInformation("Received response from Green Invoice API.");

//                var Response = JsonSerializer.Deserialize<>(result);
//                return tokenResponse.token;
//            }
//            catch (HttpRequestException e)
//            {
//                _logger.LogError(e, "HTTP request to Green Invoice API failed.");
//                throw;
//            }
//        }
//    }
//}
