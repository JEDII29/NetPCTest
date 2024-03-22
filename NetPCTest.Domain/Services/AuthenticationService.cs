using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetPCTest.Domain.Abstract;
using NetPCTest.Data.Entities;
using NetPCTest.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace NetPCTest.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthenticationService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public bool IsValidUser(string username, string password)
        {
            List<UserEntity> users = _userService.GetAllUsers().Result;
            if(users.Any(x => x.Login == username && x.PasswordHash == password))
                return true;
            return false;
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var id = await _userService.GetUserId(username);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var handler = new JsonWebTokenHandler();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);
            var claims = new List<Claim>
            {
					new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = expiration
            });

            return token;
        }
    }
}
