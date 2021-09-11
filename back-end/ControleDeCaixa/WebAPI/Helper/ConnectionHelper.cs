using System.Configuration;

namespace ControleDeCaixa.WebAPI.Helper
{
    public class ConnectionHelper : IConnectionHelper
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
