using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UsernameEstahDisponivel([FromQuery] string username)
        {
            if (await _pessoaRepositorio.UsernameEstahDisponivelAsync(username) is var resultadoValidacao && resultadoValidacao.EhFalha)
                return BadRequest(resultadoValidacao.Falha);
            return Ok(resultadoValidacao.Sucesso);
        }

        [AllowAnonymous]
        [HttpPost("NovoCadastro")]
        public async Task<IActionResult> AdicionarNovoCadastro([FromServices] ITokenJWT tokenJWT,
        [FromBody] PessoaInputModel inputModel)
        {
            if (await _pessoaHandler.NovaContaAsync(inputModel) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(new
            {
                resultado.Sucesso.Id,
                resultado.Sucesso.Avatar,
                resultado.Sucesso.Email,
                resultado.Sucesso.Username,
                resultado.Sucesso.Nome,
                DataNascimento = resultado.Sucesso.DataNascimento.ToString("dd/MM/yyyy"),
                Token = tokenJWT.GerarToken(resultado.Sucesso)
            });
        }

        [Authorize(Roles = "ContaRegistrada")]
        [HttpPut("{pessoaId}/AtualizarCadastro")]
        public async Task<IActionResult> AtualizarImagem([FromRoute] int pessoaId,
                                                         [FromBody] PessoaAtualizada pessoaAtualizada)
        {
            if (await _pessoaHandler.AtualizarContaAsync(pessoaId, pessoaAtualizada) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(PessoaResumida.ConverterParaPessoaResumida(resultado.Sucesso));
        }

        [Authorize(Roles = "ContaRegistrada")]
        [HttpPut("{pessoaId}/AlterarSenha")]
        public async Task<IActionResult> AtualizarSenhas([FromRoute] int pessoaId,
                                                         [FromBody] AlterarSenha alterarSenha)
        {
            if (await _pessoaHandler.AtualizarSenhaAsync(pessoaId, alterarSenha) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(PessoaResumida.ConverterParaPessoaResumida(resultado.Sucesso));
        }

        [Authorize(Roles = "ContaRegistrada")]
        [HttpGet("{pessoaId}")]
        public async Task<IActionResult> RecuperarPessoaPorId([FromRoute] int pessoaId)
        {
            if (await _pessoaRepositorio.RecuperarPessoaPorIdAsync(pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(PessoaResumida.ConverterParaPessoaResumida(resultado.Sucesso));
        }

        [HttpPost("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha([FromServices] ISendGridSendEmailService emailService, 
            [FromBody] dynamic pessoa)
        {
            var template = $"<h1>Olá {pessoa.Nome}</h1>";
            if (await emailService.EnviarEmailAsync("ebenau06@gmail.com", "Jão", template) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok();
        }
    }
}
