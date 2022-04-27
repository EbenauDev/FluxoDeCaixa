using ControleDeCaixa.Dominio.Models;
using ControleDeCaixa.Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Authorize(Roles = "ContaRegistrada")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public sealed class MetasController : ControllerBase
    {
        private readonly IMetasRepositorio _metasRepositorio;
        public MetasController(IMetasRepositorio metasRepositorio)
        {
            _metasRepositorio = metasRepositorio;
        }

        [HttpGet("Resumo")]
        public async Task<IActionResult> RecuperarMeta([FromQuery] int pessoaId)
        {
            if (await _metasRepositorio.RecuperarResumoMetas(pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("Nova")]
        public async Task<IActionResult> AdicionarNovaMeta([FromQuery] int pessoaId, [FromBody] NovaMeta meta)
        {
            if (await _metasRepositorio.AdicionarNovaMetaAsync(meta, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Created(nameof(AdicionarNovaMeta), meta);
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> AdicionarNovaMeta([FromQuery] int pessoaId, [FromBody] MetaAtualizada meta)
        {
            if (await _metasRepositorio.AtualizarMetaAsync(meta, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpDelete("Remover")]
        public async Task<IActionResult> RemoverMeta([FromQuery] int pessoaId, int metaId)
        {
            if (await _metasRepositorio.RemoverMetaAsync(pessoaId, metaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }
    }
}
