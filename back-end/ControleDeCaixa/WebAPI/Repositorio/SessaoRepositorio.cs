using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface ISessaoRepositorio
    {
        Task<Resultado<bool, Falha>> GravarSessaoPessoa(Sessao sessao);
    }
    public class SessaoRepositorio : ISessaoRepositorio
    {
        private readonly string _connectionString;
        public SessaoRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<bool, Falha>> GravarSessaoPessoa(Sessao sessao)
        {
            const string sql = @"INSERT INTO Sessao (Id, IdPessoa, DataDeLogin ,DataLogout)
                                 VALUES (@Id, @IdPessoa, @DataDeLogin, @DataLogout);";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        sessao.Id,
                        sessao.IdPessoa,
                        sessao.DataDeLogin,
                        sessao.DataLogout,
                    });
                    if (linhasAfetadas == 0)
                        return Falha.Nova($"Falha ao incluir um novo registro de sessão");
                    return true;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao incluir um novo registro de sessão", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
