using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.InputModel;
using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.DataAccess
{

    public class FluxoDeCaixaDataAccess : IFluxoDeCaixaDataAccess
    {
        private readonly ILogger _logger;
        private readonly string _stringDeConexao;

        public FluxoDeCaixaDataAccess(ILoggerFactory loggerFactory,
                                       IConnectionHelper connectionHelper)
        {
            _logger = loggerFactory.CreateLogger<FluxoDeCaixaDataAccess>();
            _stringDeConexao = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<int, Falha>> NovoFluxoAnualDeCaixaAsync(FluxoCaixaAnualInputModel fluxoCaixaAnualInput)
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
                        return Falha.Nova($"Houve um problema ao inserir um novo fluxo anual para {fluxoCaixaAnualInput.Ano }");
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Houve um problema ao inserir um novo fluxo anual. Execption: {exception}", ex);
                    return Falha.Nova($"Houve um problema ao inserir um novo fluxo anual para {fluxoCaixaAnualInput.Ano }");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<int, Falha>> NovoCaixaAsync(Caixa caixa)
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
                        return Falha.Nova($"Houve um problema ao inserir um caixa  para o mes de referência {caixa.MesDeReferencia}");
                    return resultado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Houve um problema ao inserir um caixa  para o mes de referência {MesReferencia}. Execption: {exception}", caixa.MesDeReferencia, ex);
                    return Falha.Nova($"Houve um problema ao inserir um caixa  para o mes de referência {caixa.MesDeReferencia}");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<string, Falha>> RecuperarCaixasPorAnoAsync(string ano)
        {
            const string sql = @"SELECT CaixaAnual.Id, 
	                                    CaixaAnual.Ano,
	                                    CaixaMes.Id AS CaixaMesId,
	                                    CaixaMes.MesReferencia
                                    FROM FluxoCaixaAnual CaixaAnual (NOLOCK)
                                    INNER JOIN CaixaMes 
                                    ON CaixaMes.FluxoCaixaAnualId = CaixaAnual.Id 
                                    WHERE CaixaAnual.Ano = @AnoDeBusca
                                    FOR JSON AUTO, ROOT('CaixaAnual');";
            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<dynamic>(sql, new { AnoDeBusca = ano });
                    if (resultado == null)
                        return "[]";
                    return JsonConvert.SerializeObject(resultado);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Houve um problema ao tentar recuperar os caixas para o ano {ano}. Execption: {exception}", ano, ex);
                    return Falha.Nova("Houve um problema ao tentar recuperar os caixas para o ano informado");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
