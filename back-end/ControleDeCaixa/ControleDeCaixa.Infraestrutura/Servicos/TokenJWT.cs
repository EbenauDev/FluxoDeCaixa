﻿using ControleDeCaixa.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleDeCaixa.Infraestrutura.Servicos
{
    public interface ITokenJWT
    {
        string GerarToken(Pessoa pessoa);
    }

    public sealed class TokenJWT : ITokenJWT
    {
        private readonly string _secretKey;
        public TokenJWT(IConfiguration configuration)
        {
            _secretKey = configuration["SecretKey"];
        }

        public string GerarToken(Pessoa pessoa)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenExpiration = DateTime.UtcNow.AddMinutes(20);
            var identity = new ClaimsIdentity(new List<Claim>
            {
                 new Claim("id", pessoa.Id.ToString()),
                 new Claim("nomeDeUsuario", pessoa.Username.ToString()),
                 new Claim("idade", (DateTime.Now.Year - pessoa.DataNascimento.Year).ToString()),
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