using ControleDeCaixa.Infraestrutura.Helper;
using ControleDeCaixa.Infraestrutura.Repositorio;
using ControleDeCaixa.Infraestrutura.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeCaixa.Infraestrutura.Infra
{
    public static class ServicosDeInfraestrutura
    {
        public static IServiceCollection RegistrarServicosInfra(this IServiceCollection services)
        {
            //Helpers
            services.AddSingleton<IConnectionHelper, ConnectionHelper>();

            //Servicos
            services.AddSingleton<ICriptografiaMD5, CriptografiaMD5>();
            services.AddSingleton<IMailService, GmailService>();
            services.AddSingleton<ITokenJWT, TokenJWT>();


            //Repositorios
            services.AddSingleton<IMovimentacoesRepositorio, MovimentacoesRepositorio>();
            services.AddSingleton<IOperacoesRepositorio, OperacoesRepositorio>();
            services.AddSingleton<ISessaoRepositorio, SessaoRepositorio>();
            services.AddSingleton<IPessoaRepositorio, PessoaRepositorio>();
            services.AddSingleton<ILoginRepositorio, LoginRepositorio>();
            services.AddSingleton<IMetasRepositorio, MetasRepositorio>();
            services.AddSingleton<IEmailRepositorio, EmailRepositorio>();
            services.AddSingleton<ISenhasRepositorio, SenhasRepositorio>();
            return services;
        }
    }
}
