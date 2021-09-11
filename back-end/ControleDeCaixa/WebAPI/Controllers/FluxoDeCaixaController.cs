using ControleDeCaixa.WebAPI.InputModel;
using ControleDeCaixa.WebAPI.Repositorio;
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
    }
}
