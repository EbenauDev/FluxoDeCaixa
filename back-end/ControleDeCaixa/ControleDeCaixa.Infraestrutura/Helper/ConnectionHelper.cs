using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace ControleDeCaixa.Infraestrutura.Helper
{
    public interface IConnectionHelper
    {
        string GetConnectionString();
    }

    public class ConnectionHelper : IConnectionHelper
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        public ConnectionHelper(IConfiguration configuration, ILoggerFactory _loggerFactory)
        {
            _configuration = configuration;
            _logger = _loggerFactory.CreateLogger<ConnectionHelper>();
        }

        public string GetConnectionString()
        {
            if (_configuration.GetConnectionString("UsoGeral") is var resultado && resultado == null)
            {
                _logger.LogError("String de conexão não foi inicializada");
                throw new ArgumentNullException("String de conexão não foi inicializada");
            }
            return resultado;
        }
    }
}
