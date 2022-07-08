using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {
        public async Task<IActionResult> CadastrarCaixaAsync()
        {
            return Ok();
        }

        public async Task<IActionResult> AtualizarCaixaAsync()
        {
            return Ok();
        }

        public async Task<IActionResult> AbrirCaixa()
        {

            return Ok();
        }

        public async Task<IActionResult> FecharCaixa()
        {

            return Ok();
        }
    }
}
