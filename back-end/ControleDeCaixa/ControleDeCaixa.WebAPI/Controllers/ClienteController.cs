using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        public async Task<IActionResult> CadastrarClienteAsync()
        {

            return Ok();
        }

        public async Task<IActionResult> LoginClienteAsync()
        {

            return Ok();
        }
    }
}
