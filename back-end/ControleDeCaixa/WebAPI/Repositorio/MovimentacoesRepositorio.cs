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
    public interface IMovimentacoesRepositorio
    {
        Task<Resultado<bool, Falha>> AnoDeMovimentacoesJahExiste(int ano, int pessoaId);
        Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacaoAsync(MovimentacaoAnual movimentacaoAnual);
    }

    public class MovimentacoesRepositorio : IMovimentacoesRepositorio
    {
        private readonly string _connectionString;
        public MovimentacoesRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<bool, Falha>> AnoDeMovimentacoesJahExiste(int ano, int pessoaId)
        {
            const string sql = @"SELECT IIF(COUNT(Id) > 0, 1, 0) AS AnoDeMovimentacoesExiste
                                 FROM MovimentacoesAnuais(NOLOCK)
                                 WHERE Ano = @ano AND IdPessoa = @pessoaId;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return await conexao.QueryFirstOrDefaultAsync<bool>(sql, new { ano, pessoaId });
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao verificar se o registro para o ano {ano} já existe", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacaoAsync(MovimentacaoAnual movimentacaoAnual)
        {
            const string sql = @"INSERT INTO MovimentacoesAnuais(IdPessoa, Ano, Descricao, DataDeCriacao)
                                 VALUES(@IdPessoa, @Ano, @Descricao, @DataDeCriacao);

                                 SELECT SCOPE_IDENTITY() AS Id;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultadoIdMovimentacaoAnual = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        IdPessoa = movimentacaoAnual.PessoaId,
                        movimentacaoAnual.Ano,
                        movimentacaoAnual.Descricao,
                        movimentacaoAnual.DataDeCriacao
                    });
                    if (resultadoIdMovimentacaoAnual == 0)
                        return Falha.Nova("Houve um problema ao tentar salvar o novo ano de movimentações");
                    movimentacaoAnual.DefinirId(resultadoIdMovimentacaoAnual);
                    return movimentacaoAnual;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao salvar o ano de movimentações", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
