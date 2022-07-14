using ControleDeCaixa.Aplicacao.Cliente;
using ControleDeCaixa.Infra.Cliente;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeCaixa.Aplicacao
{
    public static class DependencyInjection
    {
        public static void AdicionarServicosAplicacao(this IServiceCollection services)
        {
            //Cliente
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<ISalvarRegistroClienteCommand, SalvarRegistroClienteCommand>();
        }
    }
}
