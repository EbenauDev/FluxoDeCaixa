using ControleDeCaixa.Aplicacao.Handler;
using ControleDeCaixa.Dominio.Models;
using ControleDeCaixa.Infraestrutura.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginHandler _loginHandler;
        public LoginController(ILoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> AdicionarNovoCadastro([FromServices] ITokenJWT tokenJWT,
                                                               [FromBody] Login inputModel)
        {
            if (await _loginHandler.AutenticarPessoaAsync(inputModel) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(new
            {
                resultado.Sucesso.Id,
                resultado.Sucesso.Avatar,
                resultado.Sucesso.Email,
                resultado.Sucesso.Username,
                DataNascimento = resultado.Sucesso.DataNascimento.ToString("dd/MM/yyyy"),
                resultado.Sucesso.Nome,
                Token = tokenJWT.GerarToken(resultado.Sucesso)
            });
        }
    }
}
