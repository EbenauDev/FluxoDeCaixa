using ControleDeCaixa.WebAPI.Repositorio;
using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {
        private readonly IMovimentacoesHandler _movimentacoesHandler;
        private readonly IMovimentacoesRepositorio _movimentacoesRepositorio;
        public MovimentacoesController(IMovimentacoesHandler movimentacoesHandler,
                                       IMovimentacoesRepositorio movimentacoesRepositorio)
        {
            _movimentacoesHandler = movimentacoesHandler;
            _movimentacoesRepositorio = movimentacoesRepositorio;
        }

        [HttpPost("{pessoaId}/NovoAnoDeMovimentacoes")]
        public async Task<IActionResult> NovoAnoDeMovimentacoes([FromBody] MoviementacoesAnuais movimentacoes,
                                                                [FromRoute] int pessoaId)
        {
            if (await _movimentacoesHandler.NovoAnoDeMovimentacoesAsync(movimentacoes, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("{pessoaId}/MesDeMovimentacoes")]
        public async Task<IActionResult> NovaOperacaoDoMes([FromRoute] int pessoaId, [FromBody] MesDeMovimentacoes movimentacao)
        {
            if (await _movimentacoesHandler.NovoMesDeMovimentacao(movimentacao, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("{pessoaId}/OperacaoMes")]
        public async Task<IActionResult> NovaOperacaoDoMes([FromRoute] int pessoaId, [FromBody] OperacoesMes operacoesMes)
        {
            if (await _movimentacoesHandler.NovaOperacaoNoMesAsync(operacoesMes, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("{pessoaId}/ListarAnosDeMovimentacoes")]
        public async Task<IActionResult> ListarAnosDeMovimentacoesParaAPessoa([FromRoute] int pessoaId)
        {
            if (await _movimentacoesRepositorio.RecuperarAnosDeMovimentacoesAsync(pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("{pessoaId}/{anoId}/MesDeMovimentacoes")]
        public async Task<IActionResult> ListarMesesDeMovimentacoes([FromRoute] int pessoaId, int anoId)
        {
            if (await _movimentacoesRepositorio.RecuperarMesesDeMovimentacaoDoAnoAsync(pessoaId, anoId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }
    }
}
