using Application.Configurations;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {

        private readonly AppConfiguration configuration;

        public JwtService(AppConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            //create claims:
            var claims = new[]
            {
                new Claim("sub", user.Username),
                new Claim("role", user.Role.ToString())
            };

            //create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTAcessSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //create token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public string GenerateRefreshToken(User user)
        {
            //create claims:
            var claims = new[]
            {
                new Claim("sub", user.Username),
                new Claim("role", user.Role.ToString())
            };

            //create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTRefreshSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //create token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool isAccessTokenValid(string token)
        {
            throw new NotImplementedException();
        }

        public bool isRefreshTokenValid(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool isTokenExpired(string token)
        {
            return ExtractExpirationDate(token) < DateTime.UtcNow;
        }

        public DateTime ExtractExpirationDate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                // Token cannot be read or parsed
                throw new ArgumentException("Invalid token");
            }

            var expirationDate = jwtToken.ValidTo;
            return expirationDate;
        }
    }
}
