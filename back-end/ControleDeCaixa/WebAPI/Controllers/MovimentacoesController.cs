using ControleDeCaixa.WebAPI.Repositorio;
using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Entidades;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Authorize(Roles = "ContaRegistrada")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {
        private readonly IMovimentacoesHandler _movimentacoesHandler;
        private readonly IMovimentacoesRepositorio _movimentacoesRepositorio;
        public MovimentacoesController(IMovimentacoesHandler movimentacoesHandler,
                                       IMovimentacoesRepositorio movimentacoesRepositorio)
        {
            _movimentacoesHandler = movimentacoesHandler;
            _movimentacoesRepositorio = movimentacoesRepositorio;
        }

        [HttpPost("Pessoa/{pessoaId}/NovoAnoDeMovimentacoes")]
        public async Task<IActionResult> NovoAnoDeMovimentacoes([FromBody] MoviementacoesAnuais movimentacoes,
                                                                [FromRoute] int pessoaId)
        {
            if (await _movimentacoesHandler.NovoAnoDeMovimentacoesAsync(movimentacoes, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("Pessoa/{pessoaId}/MesDeMovimentacoes")]
        public async Task<IActionResult> NovaOperacaoDoMes([FromRoute] int pessoaId, [FromBody] MesDeMovimentacoes movimentacao)
        {
            if (await _movimentacoesHandler.NovoMesDeMovimentacaoAsync(movimentacao, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPost("Pessoa/{pessoaId}/OperacaoMes")]
        public async Task<IActionResult> NovaOperacaoDoMes([FromRoute] int pessoaId, [FromBody] OperacoesMes operacoesMes)
        {
            if (await _movimentacoesHandler.NovaOperacaoNoMesAsync(operacoesMes, pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpPut("Pessoa/{pessoaId}/OperacaoMes/{operacaoId}")]
        public async Task<IActionResult> AtualizarOperacaoDoMes([FromRoute] int pessoaId, int operacaoId,
                                                                [FromBody] OperacoesMes operacaoMes)
        {
            var operacao = new OperacaoMes(
                id: operacaoId,
                operacaoMes.Valor,
                operacaoMes.MesId,
                operacaoMes.Descricao,
                (ETipoOperacaoMes)Enum.Parse(typeof(ETipoOperacaoMes), operacaoMes.TipoOperacao));
            if (await _movimentacoesRepositorio.AtualizarOperacaoNoMesAsync(operacaoId, operacao) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("Pessoa/{pessoaId}/ListarAnosDeMovimentacoes")]
        public async Task<IActionResult> ListarAnosDeMovimentacoesParaAPessoa([FromRoute] int pessoaId)
        {
            if (await _movimentacoesRepositorio.RecuperarAnosDeMovimentacoesAsync(pessoaId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("Pessoa/{pessoaId}/Ano/{anoId}/MesDeMovimentacoes")]
        public async Task<IActionResult> ListarMesesDeMovimentacoes([FromRoute] int pessoaId, int anoId)
        {
            if (await _movimentacoesRepositorio.RecuperarMesesDeMovimentacaoDoAnoAsync(pessoaId, anoId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("Pessoa/{pessoaId}/Ano/{anoId}/Mes/{mesId}/Movimentacoes")]
        public async Task<IActionResult> ListarMovimentacoesDoMesAsync([FromRoute] int pessoaId, int anoId, int mesId)
        {
            if (await _movimentacoesRepositorio.ListarMovimentacoesDoMesAsync(anoId, mesId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpDelete("Mes/{mesId}/Movimentacao/{operacaoMesId}")]
        public async Task<IActionResult> DeletarOperacaoDoMes([FromRoute] int mesId, int operacaoMesId)
        {
            if (await _movimentacoesRepositorio.ExcluirOperacaoDoMesAsync(mesId, operacaoMesId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }

        [HttpGet("Pessoa/{pessoaId}/Ano/{anoId}/Historico")]
        public async Task<IActionResult> ListarMovimentacoesDoMesAsync([FromRoute] int pessoaId, int anoId)
        {
            if (await _movimentacoesRepositorio.HistoricoMovimentacoesAsync(pessoaId, anoId) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }
    }
}
