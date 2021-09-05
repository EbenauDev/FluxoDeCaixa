using System.Configuration;

namespace ControleDeCaixa.WebAPI.Infra
{
    public static class Connection
    {
        public static string ConnectionValue()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
