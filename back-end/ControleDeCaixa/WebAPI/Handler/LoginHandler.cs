using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Handler
{
    public interface ILoginHandler
    {
        Task<Resultado<Pessoa, Falha>> AutenticarPessoaAsync(Login login);
    }

    public class LoginHandler : ILoginHandler
    {
        private readonly ILoginRepositorio _loginRepositorio;
        private readonly ILogger _logger;
        private readonly ICriptografiaMD5 _criptografiaMD5;
        public LoginHandler(ILoginRepositorio loginRepositorio,
            ILoggerFactory factory,
            ICriptografiaMD5 criptografiaMD5)
        {
            _loginRepositorio = loginRepositorio;
            _logger = factory.CreateLogger<LoginHandler>();
            _criptografiaMD5 = criptografiaMD5;
        }

        public async Task<Resultado<Pessoa, Falha>> AutenticarPessoaAsync(Login login)
        {
            if (await _loginRepositorio.RecuperarContaAsync(login) is var resultadoConsulta && resultadoConsulta.EhFalha)
            {
                _logger.LogError("Falha ao autenticar conta. Falha: {falha}", resultadoConsulta.Falha);
                return Falha.Nova("Falha ao autenticar conta");
            }
            if (_criptografiaMD5.ConverterParaMD5(login.Senha) is var resultadoSenha && resultadoSenha == null)
            {
                _logger.LogError($"Falha ao converter a senha {login.Senha}");
                return Falha.Nova("Falha ao realizar autenticação.");
            }
            if (resultadoSenha.Equals(resultadoConsulta.Sucesso.Senha))
                return resultadoConsulta.Sucesso;
            return Falha.Nova("Username ou Senha são inválidos");
        }
    }
}
