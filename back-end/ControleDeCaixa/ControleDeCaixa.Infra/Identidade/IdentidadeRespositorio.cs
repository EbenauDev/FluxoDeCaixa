using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Core.Utils;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Identidade
{

    public interface IIdentidadeRepositorio
    {
        public Task<Resultado<bool, Falha>> UsuarioEstahDisponivel(string usuario);
    }

    public class IdentidadeRespositorio : IIdentidadeRepositorio
    {
        private readonly string _stringConexao;
        public IdentidadeRespositorio(IConnectionStringHelper _helper)
        {
            _stringConexao = _helper.GetConnection();
        }

        public async Task<Resultado<bool, Falha>> UsuarioEstahDisponivel(string usuario)
        {
            const string sql = @"EXEC dbo.UsuarioEstahDisponivel @usuario;";
            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<bool>(sql, usuario);

                    if (resultado is false)
                        return Falha.Nova($"O username {usuario} já está em uso");
                    return resultado;
                }
                catch (Exception ex)
                {
                    return Falha.Nova("Falha ao consultar se o usuário está disponível", Falha.Nova(ex.Message));
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
