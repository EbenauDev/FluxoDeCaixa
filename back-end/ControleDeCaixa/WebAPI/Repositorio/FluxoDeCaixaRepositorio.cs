using ControleDeCaixa.WebAPI.Entites;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
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
                        var historico = await HistoricoMaisRecenteAsync(conexao, transacao, operacaoCaixa.CaixaId);
                        if (operacaoCaixa.Operacao == ETipoOperacaoCaixa.Entrada)
                            historico.AtualizarReceitas(operacaoCaixa.Valor);
                        else
                            historico.AtualizarCustos(operacaoCaixa.Valor);
                        var resultadoHistorico = await GerarHistoricoAsync(conexao, transacao, historico);
                        await transacao.CommitAsync();
                        return Resultado<bool, Falha>.NovoSucesso(true);
                    }
                    catch (Exception ex)
                    {
                        await transacao.RollbackAsync();
                        _logger.LogError("Houve um problema ao inserir uma nova operacao para o caixa {CaixaId}. Execption: {exception}", operacaoCaixa.CaixaId, ex);
                        return Resultado<bool, Falha>.NovaFalha(Falha.NovaComException(400, $"Houve um problema ao inserir uma nova operacao para o caxa {operacaoCaixa.CaixaId}", ex));
                    }
                    finally
                    {
                        await conexao.CloseAsync();
                    }
                }
            }
        }

        private async Task<int> InserirOperacaoAsync(SqlConnection conexao, DbTransaction transaction, OperacaoCaixa operacaoCaixa)
        {
            const string insertOperacao = @"INSERT INTO OperacaoCaixa(TipoOperacao, Valor, Descricao, CaixaMesId)
                                            VALUES(@TipoOperacao, @Valor, @Descricao, @CaixaMesId);";
            var linhasAfetadas = await conexao.ExecuteAsync(insertOperacao, new
            {
                TipoOperacao = (char)operacaoCaixa.Operacao,
                Valor = operacaoCaixa.Valor,
                Descricao = operacaoCaixa.Descricao,
                CaixaMesId = operacaoCaixa.CaixaId
            }, transaction);
            if (linhasAfetadas <= 0)
                throw new Exception("Falha ao inserir a operação de caixa");
            return linhasAfetadas;
        }

        private async Task<HistoricoCaixaViewModel> HistoricoMaisRecenteAsync(SqlConnection conexao, DbTransaction transaction, int CaixaMesId)
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

        private async Task<int> GerarHistoricoAsync(SqlConnection conexao, DbTransaction transaction, HistoricoCaixaViewModel historico)
        {

            const string gerarHistorico = @"INSERT INTO HistoricoCaixa(TotalReceitas, TotalCustos, Data, CaixaMesId)
                                            VALUES(@TotalReceitas, @TotalCustos, @Data, @CaixaMesId)";


            var linhasAfetadas = await conexao.ExecuteAsync(gerarHistorico, new
            {
                historico.TotalReceitas,
                historico.TotalCustos,
                Data = DateTime.Now,
                CaixaMesId = historico.CaixaMesId
            }, transaction);
            if (linhasAfetadas <= 0)
                throw new Exception("Falha ao inserir o histórico de caixa");
            return linhasAfetadas;
        }

        public class HistoricoCaixaViewModel
        {
            //Construtor default para funcionar com o DAPPER
            //TODO: Refatorar isso e consultar mais sobre reflection
            public HistoricoCaixaViewModel()
            {

            }
            public HistoricoCaixaViewModel(double totalReceitas, double totalCustos, int caixaMesId)
            {
                TotalReceitas = totalReceitas;
                TotalCustos = totalCustos;
                CaixaMesId = caixaMesId;
            }


            public double TotalReceitas { get; set; }
            public double TotalCustos { get; set; }
            public int CaixaMesId { get; set; }

            public void AtualizarReceitas(double receitas) => TotalReceitas += receitas;
            public void AtualizarCustos(double custos) => TotalCustos += custos;
        }
    }
}
