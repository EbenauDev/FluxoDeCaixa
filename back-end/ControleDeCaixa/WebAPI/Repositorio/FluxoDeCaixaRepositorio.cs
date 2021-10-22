using ControleDeCaixa.WebAPI.Entites;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.ViewModels;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{

    public partial class FluxoDeCaixaRepositorio : IFluxoDeCaixaRepositorio
    {
        private readonly ILogger _logger;
        private readonly string _stringDeConexao;

        public FluxoDeCaixaRepositorio(ILoggerFactory loggerFactory,
                                       IConnectionHelper connectionHelper)
        {
            _logger = loggerFactory.CreateLogger<FluxoDeCaixaRepositorio>();
            _stringDeConexao = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<bool, Falha>> IncluirOperacaoCaixaAsync(OperacaoCaixa operacaoCaixa)
        {
            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                await conexao.OpenAsync();
                using (var transacao = await conexao.BeginTransactionAsync())
                {
                    try
                    {
                        await InserirOperacaoAsync(conexao, transacao, operacaoCaixa);
                        if (await HistoricoMaisRecenteAsync(conexao, transacao, operacaoCaixa.CaixaId) is var historico && historico.EhFalha)
                            return historico.Falha;
                        if (operacaoCaixa.Operacao == ETipoOperacaoCaixa.Entrada)
                            historico.Sucesso.AtualizarReceitas(operacaoCaixa.Valor);
                        else
                            historico.Sucesso.AtualizarCustos(operacaoCaixa.Valor);
                        if (await GerarHistoricoAsync(conexao, transacao, historico.Sucesso) is var resultadoHistorico && resultadoHistorico.EhFalha)
                            throw new Exception(resultadoHistorico.Falha.Mensagem);
                        await transacao.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transacao.RollbackAsync();
                        _logger.LogError("Houve um problema ao inserir uma nova operacao para o caixa {CaixaId}. Execption: {exception}", operacaoCaixa.CaixaId, ex);
                        return Falha.Nova($"Houve um problema ao inserir uma nova operacao para o caxa {operacaoCaixa.CaixaId}");
                    }
                    finally
                    {
                        await conexao.CloseAsync();
                    }
                }
            }
        }

        private async Task<Resultado<int, Falha>> InserirOperacaoAsync(SqlConnection conexao, DbTransaction transaction, OperacaoCaixa operacaoCaixa)
        {
            const string insertOperacao = @"INSERT INTO OperacaoCaixa(TipoOperacao, Valor, Descricao, CaixaMesId)
                                            VALUES(@TipoOperacao, @Valor, @Descricao, @CaixaMesId);";
            var linhasAfetadas = await conexao.ExecuteAsync(insertOperacao, new
            {
                TipoOperacao = (char)operacaoCaixa.Operacao,
                operacaoCaixa.Valor,
                operacaoCaixa.Descricao,
                operacaoCaixa.CaixaId
            }, transaction);
            if (linhasAfetadas <= 0)
                return Falha.Nova("Falha ao inserir a operação de caixa");
            return linhasAfetadas;
        }

        private async Task<Resultado<HistoricoCaixaViewModel, Falha>> HistoricoMaisRecenteAsync(SqlConnection conexao, DbTransaction transaction, int CaixaMesId)
        {
            const string historicoMaisRecente = @"SELECT TotalReceitas, 
                                                         TotalCustos, 
                                                         CaixaMesId 
                                                  FROM HistoricoCaixa(NOLOCK) WHERE CaixaMesId = @CaixaMesId 
                                                  ORDER BY Data DESC";

            var resultado = await conexao.QueryFirstOrDefaultAsync<HistoricoCaixaViewModel>(historicoMaisRecente, new { CaixaMesId }, transaction);
            if (resultado == null)
                return new HistoricoCaixaViewModel(0, 0, CaixaMesId);
            return resultado;
        }

        private async Task<Resultado<int, Falha>> GerarHistoricoAsync(SqlConnection conexao, DbTransaction transaction, HistoricoCaixaViewModel historico)
        {

            const string gerarHistorico = @"INSERT INTO HistoricoCaixa(TotalReceitas, TotalCustos, Data, CaixaMesId)
                                            VALUES(@TotalReceitas, @TotalCustos, @Data, @CaixaMesId)";

            var linhasAfetadas = await conexao.ExecuteAsync(gerarHistorico, new
            {
                historico.TotalReceitas,
                historico.TotalCustos,
                DateTime.Now,
                historico.CaixaMesId
            }, transaction);
            if (linhasAfetadas <= 0)
                return Falha.Nova("Falha ao inserir o histórico de caixa");
            return linhasAfetadas;
        }

    }
}
