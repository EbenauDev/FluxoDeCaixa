using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> AdicionarNovaMeta([FromQuery] int pessoaId, [FromBody] NovaMeta meta)
        {
            if (await _metasRepositorio.AdicionarNovaMetaAsync(meta, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Created(nameof(AdicionarNovaMeta), meta);
        }
    }
}
