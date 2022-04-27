using ControleDeCaixa.Aplicacao.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeCaixa.Aplicacao
{
    public static class ServicosDeAplicacao
    {
        public static IServiceCollection RegistrarServicosAplicacao(this IServiceCollection services)
        {
            //Servicos
            services.AddSingleton<ILoginHandler, LoginHandler>();
            services.AddSingleton<IMovimentacoesHandler, MovimentacoesHandler>();
            services.AddSingleton<PessoaHandler, PessoaHandler>();

            return services;
        }
    }
}
