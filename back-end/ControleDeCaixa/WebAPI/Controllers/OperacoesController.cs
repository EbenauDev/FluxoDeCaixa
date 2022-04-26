using ControleDeCaixa.WebAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacoesController : ControllerBase
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;

        public OperacoesController(IOperacoesRepositorio operacoesRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
        }

        [HttpGet("OperacoesTransacao")]
        public async Task<IActionResult> RecuperarOperacoesTransacaoAsync()
        {
            if (await _operacoesRepositorio.RecuperarOperacoesTransacaoAsync() is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }
    }
}
