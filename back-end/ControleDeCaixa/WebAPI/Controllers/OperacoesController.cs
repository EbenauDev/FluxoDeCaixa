using ControleDeCaixa.Dominio.Models;
using ControleDeCaixa.Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("OperacoesTransacao")]
        //OperacaoTransacaoInputModel
        public async Task<IActionResult> RecuperarOperacoesTransacaoAsync([FromBody] OperacaoTransacaoInputModel inputModel)
        {
            if (await _operacoesRepositorio.NovaOperacoesTransacaoAsync(inputModel) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

    }
}
