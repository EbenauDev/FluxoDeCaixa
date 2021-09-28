using ControleDeCaixa.WebAPI.Generics;
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

        public async Task<Resultado<bool, Falha>> IncluirOperacaoCaixaAsync(OperacaoCaixaInputModel operacaoCaixaInput)
        {
            throw new NotImplementedException();
        }
    }
}
