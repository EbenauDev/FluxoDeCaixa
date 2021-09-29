﻿using ControleDeCaixa.WebAPI.InputModel;
using ControleDeCaixa.WebAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Handler;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FluxoDeCaixaController : ControllerBase
    {
        private readonly IFluxoDeCaixaDataAccess _fluxoDeCaixaRepositorio;
        private readonly IOperacaoCaixaHandler _operacaoCaixaHandler;
        public FluxoDeCaixaController(IFluxoDeCaixaDataAccess fluxoDeCaixaRepositorio,
                                      IOperacaoCaixaHandler operacaoCaixaHandler)
        {
            _fluxoDeCaixaRepositorio = fluxoDeCaixaRepositorio;
            _operacaoCaixaHandler = operacaoCaixaHandler;
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
