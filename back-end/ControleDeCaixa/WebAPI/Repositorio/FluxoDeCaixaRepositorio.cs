using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.InputModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class FluxoDeCaixaRepositorio : IFluxoDeCaixaRepositorio
    {

        private readonly ILogger _logger;
        private readonly string _stringDeConexao;

        public FluxoDeCaixaRepositorio(ILoggerFactory loggerFactory,
                                       IConnectionHelper connectionHelper)
        {
            _logger = loggerFactory.CreateLogger<FluxoDeCaixaRepositorio>();
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
    }
}
