using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Repositorio;
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
        private ICaixaRepositorio _caixaRepositorio;
        public CaixaController(ICaixaRepositorio caixaRepositorio)
        {
            _caixaRepositorio = caixaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarFluxoCaixaAsync()
        {
            var resultado = await _caixaRepositorio.RecuperarFluxoDeCaixaAsync();
            return Ok(resultado);
        }
    }
}
