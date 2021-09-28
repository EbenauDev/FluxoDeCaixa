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
        private readonly IFluxoDeCaixaRepositorio _fluxoDeCaixaRepositorio;
        public FluxoDeCaixaController(IFluxoDeCaixaRepositorio fluxoDeCaixaRepositorio)
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

        [HttpPost("NovaReceita")]
        public async Task<IActionResult> NovaReceita([FromBody] OperacaoCaixaInputModel operacaoCaixaInput)
        {
            return Ok();
        }

        [HttpPost("NovoCusto")]
        public async Task<IActionResult> NovoCusto([FromBody] OperacaoCaixaInputModel operacaoCaixaInput)
        {
            return Ok();
        }
    }
}
