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


        [HttpGet("Ano/{ano}")]
        public async Task<IActionResult> RecuperarFluxoCaixaAnual(string ano)
        {
            var resultado = await _fluxoDeCaixaRepositorio.RecuperarFluxoDeCaixaAsync(ano);
            return Ok(resultado);
        }
    }
}
