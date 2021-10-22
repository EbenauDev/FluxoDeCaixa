using ControleDeCaixa.WebAPI.InputModel;
using ControleDeCaixa.WebAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Handler;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FluxoDeCaixaController : ControllerBase
    {
        private readonly IFluxoDeCaixaDataAccess _fluxoDeCaixaDataAccess;
        private readonly IOperacaoCaixaHandler _operacaoCaixaHandler;
        public FluxoDeCaixaController(IFluxoDeCaixaDataAccess fluxoDeCaixaRepositorio,
                                      IOperacaoCaixaHandler operacaoCaixaHandler)
        {
            _fluxoDeCaixaDataAccess = fluxoDeCaixaRepositorio;
            _operacaoCaixaHandler = operacaoCaixaHandler;
        }

        [HttpPost]
        public async Task<IActionResult> NovoFluxoAnualDeCaixa([FromBody] FluxoCaixaAnualInputModel fluxoCaixaAnualInput)
        {
            if (await _fluxoDeCaixaDataAccess.NovoFluxoAnualDeCaixaAsync(fluxoCaixaAnualInput) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("{ano}/Caixas")]
        public async Task<IActionResult> RecuperarCaixasDoAno([FromRoute] string ano)
        {
            if (await _fluxoDeCaixaDataAccess.RecuperarCaixasPorAnoAsync(ano) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("NovaReceita")]
        public async Task<IActionResult> NovaReceita([FromBody] OperacaoCaixaInputModel operacaoCaixaInput)
        {
            if (await _operacaoCaixaHandler.IncluirOperacaoCaixaAsync(operacaoCaixaInput) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return CreatedAtAction(nameof(NovaReceita), operacaoCaixaInput);
        }

        [HttpPost("NovoCusto")]
        public async Task<IActionResult> NovoCusto([FromBody] OperacaoCaixaInputModel operacaoCaixaInput)
        {
            if (await _operacaoCaixaHandler.IncluirOperacaoCaixaAsync(operacaoCaixaInput) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return CreatedAtAction(nameof(NovaReceita), operacaoCaixaInput);
        }
    }
}
