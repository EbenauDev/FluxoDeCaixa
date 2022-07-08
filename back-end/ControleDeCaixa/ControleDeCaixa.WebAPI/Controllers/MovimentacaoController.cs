using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {

        [HttpPost("{caixaId}/Todas")]
        public async Task<IActionResult> ListarMovimentacoesAsync([FromRoute] Guid caixaId)
        {
            return Ok();
        }

        [HttpPost("{caixaId}/Cadastrar")]
        public async Task<IActionResult> CadastrarMovimentacaoAsync([FromRoute] Guid caixaId)
        {
            return Ok();
        }

        [HttpPut("{caixaId}/Atualizar/{movimentacaoId}")]
        public async Task<IActionResult> AtualizarMovimentacaoAsync([FromRoute] Guid caixaId, Guid movimentacaoId)
        {
            return Ok();
        }

        [HttpDelete("{caixaId}/Remover/{movimentacaoId}")]
        public async Task<IActionResult> RemoverMovimentacaoAsync([FromRoute] Guid caixaId, Guid movimentacaoId)
        {
            return Ok();
        }
    }
}
