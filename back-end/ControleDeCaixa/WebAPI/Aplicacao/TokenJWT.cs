using ControleDeCaixa.WebAPI.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
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
        private readonly IConfiguration _configuration;
        public TokenJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Pessoa pessoa)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveSecreta = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", pessoa.Id.ToString()),
                        new Claim(ClaimTypes.Name, pessoa.Username),
                        new Claim("Perfil", "Registrada")
                    }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(chaveSecreta),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
