using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly string _connectionString;
        public PessoaRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<Pessoa, Falha>> NovaPessoaAsync(Pessoa pessoa)
        {
            const string sql = @"INSERT INTO Pessoa (Email, Senha, Username, Avatar)
                                 VALUES(@Email, @Senha, @Username, @Avatar);
                                 
                                 SELECT SCOPE_IDENTITY() AS Id;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var idConfiguracaoPessoa = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        pessoa.Email,
                        pessoa.Senha,
                        pessoa.Username,
                        pessoa.Avatar
                    });
                    if (idConfiguracaoPessoa == 0)
                        return Falha.Nova($"Houve um problema ao tentar criar a conta para {pessoa.Username}");
                    pessoa.DefinirId(idConfiguracaoPessoa);
                    return pessoa;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao incluir um novo registro para {pessoa.Username}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
