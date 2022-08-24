using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevFreela.Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string email, string role)
        {
            //Obtem informações de configuração
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            //Define algoritmo e chave a ser usada
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Define as Claims
            var claims = new List<Claim>()
            {
                new Claim("username", email),
                new Claim(ClaimTypes.Role, role)
            };

            //Inicia Token
            var token = new JwtSecurityToken(issuer: issuer, 
                                             audience: audience, 
                                             expires: DateTime.Now.AddHours(1), 
                                             signingCredentials:credentials,
                                             claims: claims);

            //Escreve token e retorna em formato string
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;   
        }
    }
}