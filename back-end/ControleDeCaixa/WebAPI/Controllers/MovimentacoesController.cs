using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
