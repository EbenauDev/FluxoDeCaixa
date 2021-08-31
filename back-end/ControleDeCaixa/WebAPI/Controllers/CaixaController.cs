using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> RecuperarFluxoCaixaAsync()
        {

        }
    }
}
