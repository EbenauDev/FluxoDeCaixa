using System.Configuration;

namespace ControleDeCaixa.WebAPI.Helper
{
    public static class Connection
    {
        public static string ConnectionValue()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
