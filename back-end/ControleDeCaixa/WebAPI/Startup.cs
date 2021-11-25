using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Repositorio;
using ControleDeCaixa.WebAPI.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IPessoaRepositorio, PessoaRepositorio>();
            services.AddSingleton<IPessoaHandler, PessoaHandler>();
            services.AddSingleton<ICriptografiaMD5, CriptografiaMD5>();
            services.AddSingleton<IConnectionHelper, ConnectionHelper>();
            services.AddSingleton<ITokenJWT, TokenJWT>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
