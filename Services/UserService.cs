using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MorningIntegration.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MorningIntegration.Interface;

namespace MorningIntegration.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        public UserService (IConfiguration config)
        {
            _config = config;
        }

        private bool ValidateUser(string userName, string password)
        {
            return userName == "admin" && password == "admin";
        }

        public async Task<string> AuthenticateAsync(User user)
        {
            if (!ValidateUser(user.UserName, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}

