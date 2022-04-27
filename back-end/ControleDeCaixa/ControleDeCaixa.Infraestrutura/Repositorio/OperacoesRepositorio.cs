using ControleDeCaixa.Compartilhado.Generics;
using ControleDeCaixa.Dominio.Entidades;
using ControleDeCaixa.Dominio.Views;
using ControleDeCaixa.Infraestrutura.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infraestrutura.Repositorio
{

    public interface IOperacoesRepositorio
    {
        Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacaoMes operacaoMes);
        Task<Resultado<OperacaoMes, Falha>> AtualizarOperacaoNoMesAsync(int operacaoMesId, OperacaoMes operacaoMes);
        Task<Resultado<IEnumerable<OperacaoTransacaoViewModel>, Falha>> RecuperarOperacoesTransacaoAsync();
    }

    public class OperacoesRepositorio : IOperacoesRepositorio
    {
        private readonly string _connectionString;
        public OperacoesRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacaoMes operacaoMes)
        {
            const string sql = @"INSERT INTO OperacoesDoMes(Valor, MesId, Descricao, DataDeRegistro, OperacaoTransacaoId)
                                 VALUES(@Valor, @MesId, @Descricao, @DataDeRegistro, @OperacaoTransacaoId);

                                 SELECT SCOPE_IDENTITY() AS Id;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var idNovaMovimentacao = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        operacaoMes.Valor,
                        operacaoMes.MesId,
                        operacaoMes.Descricao,
                        DataDeRegistro = DateTime.Now,
                        operacaoMes.OperacaoTransacaoId
                    });
                    if (idNovaMovimentacao <= 0)
                        return Falha.Nova("Houve um problema ao tentar salvar o novo ano de movimentações");
                    operacaoMes.DefinirId(idNovaMovimentacao);
                    return operacaoMes;
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


        public async Task<Resultado<OperacaoMes, Falha>> AtualizarOperacaoNoMesAsync(int operacaoMesId, OperacaoMes operacao)
        {
            const string sql = @"UPDATE OperacoesDoMes
	                                   SET Valor = @Valor,
		                                   Descricao = @Descricao,
                                           OperacaoTransacaoId = @OperacaoTransacaoId,
                                           DataDeRegistro = @DataDeRegistro,
                                 WHERE Id =  @operacaoMesId;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        operacaoMesId,
                        operacao.Valor,
                        operacao.Descricao,
                        operacao.OperacaoTransacaoId,
                        DataDeRegistro = DateTime.Now
                    });
                    if (linhasAfetadas <= 0)
                        return Falha.Nova("Falha ao atualizar operação do mês");
                    operacao.DefinirId(operacaoMesId);
                    return operacao;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao atualizar operação do mês", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<IEnumerable<OperacaoTransacaoViewModel>, Falha>> RecuperarOperacoesTransacaoAsync()
        {
            const string sql = @"SELECT Id, 
	                                    Nome, 
	                                    Descricao, 
	                                    Tag 
                                FROM OperacaoTransacao(NOLOCK);";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return (await conexao.QueryAsync<OperacaoTransacaoViewModel>(sql)).ToList();
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao recuperar as operações transação", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }


    }
}
