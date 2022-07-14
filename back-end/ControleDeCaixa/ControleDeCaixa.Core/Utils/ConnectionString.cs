using Microsoft.Extensions.Configuration;

namespace ControleDeCaixa.Core.Utils
{
    public interface IConnectionStringHelper
    {
        string GetConnection();
        string GetConnection(string enviroment);
    }

    public class ConnectionStringHelper : IConnectionStringHelper
    {
        private readonly IConfiguration _configuration;
        public ConnectionStringHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            return _configuration.GetConnectionString("Default");
        }

        public string GetConnection(string enviroment)
        {
            return _configuration.GetConnectionString(enviroment);
        }
    }
}
