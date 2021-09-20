using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.InputModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.DataAccess
{

    public class FluxoDeCaixaDAO : IFluxoDeCaixaDAO    {
        private readonly ILogger _logger;
        private readonly string _stringDeConexao;

        public FluxoDeCaixaDAO(ILoggerFactory loggerFactory,
                                       IConnectionHelper connectionHelper)
        {
            _logger = loggerFactory.CreateLogger<FluxoDeCaixaDAO>();
            _stringDeConexao = connectionHelper.GetConnectionString();
        }

        public async Task<int> NovoFluxoAnualDeCaixaAsync(FluxoCaixaAnualInputModel fluxoCaixaAnualInput)
        {
            const string sql = @"INSERT INTO FluxoCaixaAnual(Ano)
                                 VALUES (@Ano);
                                 SELECT SCOPE_IDENTITY() AS Id;";

            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    if (await conexao.QueryFirstOrDefaultAsync<int>(sql, new { fluxoCaixaAnualInput.Ano }) is var resultado && resultado <= 0)
                        throw new Exception($"Houve um problema ao inserir um novo fluxo anual para {fluxoCaixaAnualInput.Ano }");
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Houve um problema ao inserir um novo fluxo anual. Execption: {exception}", ex);
                    throw new Exception($"Houve um problema ao inserir um novo fluxo anual para {fluxoCaixaAnualInput.Ano }");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<int> NovoCaixaAsync(Caixa caixa)
        {
            const string sql = @"INSERT INTO CaixaMes(MesReferencia, FluxoCaixaAnualId)
                                 VALUES (@MesReferencia, @FluxoCaixaAnualId);
                                 SELECT SCOPE_IDENTITY() AS Id;";

            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        MesReferencia = caixa.MesDeReferencia,
                        FluxoCaixaAnualId = caixa.FluxoAnualId
                    });
                    if (resultado <= 0)
                        throw new Exception($"Houve um problema ao inserir um caixa  para o mes de referência {caixa.MesDeReferencia}");
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Houve um problema ao inserir um caixa  para o mes de referência {MesReferencia}. Execption: {exception}", caixa.MesDeReferencia, ex);
                    throw new Exception($"Houve um problema ao inserir um caixa  para o mes de referência {caixa.MesDeReferencia }");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
