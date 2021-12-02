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
        public MovimentacoesController(IMovimentacoesHandler movimentacoesHandler)
        {
            _movimentacoesHandler = movimentacoesHandler;
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
    }
}
