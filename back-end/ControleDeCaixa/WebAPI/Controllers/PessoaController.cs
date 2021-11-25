using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaHandler _pessoaHandler;
        private readonly IPessoaRepositorio _pessoaRepositorio;
        public PessoaController(IPessoaHandler pessoaHandler,
                                IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaHandler = pessoaHandler;
            _pessoaRepositorio = pessoaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> UsernameEstahDisponivel([FromQuery] string username)
        {
            if (await _pessoaRepositorio.UsernameEstahDisponivelAsync(username) is var resultadoValidacao && resultadoValidacao.EhFalha)
                return BadRequest(resultadoValidacao.Falha);
            return Ok(resultadoValidacao.Sucesso);
        }

        [HttpPost("NovoCadastro")]
        public async Task<IActionResult> AdicionarNovoCadastro([FromServices] ITokenJWT tokenJWT,
        [FromBody] PessoaInputModel inputModel)
        {
            if (await _pessoaHandler.NovaContaAsync(inputModel) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(new
            {
                resultado.Sucesso.Avatar,
                resultado.Sucesso.Email,
                resultado.Sucesso.Username,
                Token = tokenJWT.GerarToken(resultado.Sucesso)
            });
        }

        [HttpPut("AtualizarCadastro")]
        public async Task<IActionResult> AtualizarCadastro([FromQuery] int id, 
                                                           [FromBody] PessoaAtualizada pessoaAtualizada)
        {
            if (await _pessoaHandler.AtualizarContaAsync(id, pessoaAtualizada) is var resultadoValidacao && resultadoValidacao.EhFalha)
                return BadRequest(resultadoValidacao.Falha);
            return Ok(resultadoValidacao.Sucesso);
        }

    }
}
