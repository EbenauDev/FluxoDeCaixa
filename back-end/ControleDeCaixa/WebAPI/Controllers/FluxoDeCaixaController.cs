using ControleDeCaixa.WebAPI.InputModel;
using ControleDeCaixa.WebAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FluxoDeCaixaController : ControllerBase
    {
        private readonly IFluxoDeCaixaDAO _fluxoDeCaixaRepositorio;
        public FluxoDeCaixaController(IFluxoDeCaixaDAO fluxoDeCaixaRepositorio)
        {
            _fluxoDeCaixaRepositorio = fluxoDeCaixaRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> NovoFluxoAnualDeCaixa([FromBody] FluxoCaixaAnualInputModel fluxoCaixaAnualInput)
        {
            return Ok(await _fluxoDeCaixaRepositorio.NovoFluxoAnualDeCaixaAsync(fluxoCaixaAnualInput));
        }

        [HttpPost("CaixaMes")]
        public async Task<IActionResult> NovoCaixaMes([FromBody] Caixa caxaInputModel)
        {
            return Ok(await _fluxoDeCaixaRepositorio.NovoCaixaAsync(caxaInputModel));
        }
    }
}
