using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MorningIntegration.Data;
using MorningIntegration.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MorningIntegration.Interface;
using Microsoft.AspNetCore.Hosting;


namespace MorningIntegration.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AccountService> _logger;


        public AccountService(UserManager<IdentityUser> userManager, IConfiguration config, DataContext context, IHttpClientFactory httpClientFactory, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
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
                var response = await client.PostAsync("https://sandbox.d.greeninvoice.co.il/api/v1/account/token", content);

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

        public class TokenResponse
        {
            public string token { get; set; }
            public int expires { get; set; }
        }






        //private List<LoginUser> _users = new List<LoginUser>
        //{
        //    new LoginUser { Id = 1, Email = "yark222@gmail.com", Password = "123", Role = "admin"},
        //    new LoginUser { Id = 2, Email = "yarko222@gmail.com", Password = "456", Role = "guest"}
        //};


        //public async Task<String> Login(string email, string password)
        //{
        //    //var user = await _context.Accounts.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

        //    // return null if user not found
        //    //if (user == null)
        //    //{
        //    //    return null;
        //    //}

        //    // authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Email),
        //            new Claim(ClaimTypes.Role, "user")
        //        }),

        //        Expires = DateTime.UtcNow.AddMinutes(5),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //user.Token = tokenHandler.WriteToken(token);

        //    //return user.Token;
        //    return tokenHandler.WriteToken(token);
        //}



    }
}