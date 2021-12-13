using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Repositorio;
using ControleDeCaixa.WebAPI.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "ConfiguracaoDeCors",
                    builder =>
                    {
                        builder.
                        WithOrigins("http://localhost:8080", "https://ui-movimentacoes.azurewebsites.net/")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services
           .AddControllers()
           .AddNewtonsoftJson(options =>
               options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));

            var key = Encoding.ASCII.GetBytes(Configuration["SecretKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IPessoaRepositorio, PessoaRepositorio>();
            services.AddSingleton<IPessoaHandler, PessoaHandler>();
            services.AddSingleton<ICriptografiaMD5, CriptografiaMD5>();
            services.AddSingleton<IConnectionHelper, ConnectionHelper>();
            services.AddSingleton<ITokenJWT, TokenJWT>();
            services.AddSingleton<ILoginRepositorio, LoginRepositorio>();
            services.AddSingleton<ISessaoRepositorio, SessaoRepositorio>();
            services.AddSingleton<ILoginHandler, LoginHandler>();
            services.AddSingleton<IMovimentacoesRepositorio, MovimentacoesRepositorio>();
            services.AddSingleton<IMovimentacoesHandler, MovimentacoesHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("ConfiguracaoDeCors");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
