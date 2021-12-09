using ControleDeCaixa.WebAPI.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleDeCaixa.WebAPI.Aplicacao
{
    public interface ITokenJWT
    {
        string GerarToken(Pessoa pessoa);
    }

    public sealed class TokenJWT : ITokenJWT
    {
        private readonly string _secretKey = String.Empty;

        public string GerarToken(Pessoa pessoa)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenExpiration = DateTime.UtcNow.AddMinutes(25);
            var identity = new ClaimsIdentity(new List<Claim>
            {
                 new Claim(ClaimTypes.Name, pessoa.Username.ToString()),
                 new Claim(ClaimTypes.Role, "ContaRegistrada")
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
