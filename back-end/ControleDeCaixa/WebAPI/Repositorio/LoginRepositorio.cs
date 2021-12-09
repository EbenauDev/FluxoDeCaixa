using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface ILoginRepositorio
    {
        Task<Resultado<Pessoa, Falha>> RecuperarContaAsync(Login login);
    }

    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly string _connectionString;
        public LoginRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<Pessoa, Falha>> RecuperarContaAsync(Login login)
        {
            const string sql = @"SELECT Id,
                                        Nome,
	                                    DataNascimento,
                                        Avatar,
	                                    Senha,
	                                    Username,
                                        Email
                                 FROM Pessoa(NOLOCK)
                                 WHERE Username = @username;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<Pessoa>(sql, new { login.Username });
                    if (resultado is null)
                        return Falha.Nova($"Nenhuma conta está associada ao {login.Username}");
                    return resultado;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Nenhuma conta está associada ao {login.Username}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
