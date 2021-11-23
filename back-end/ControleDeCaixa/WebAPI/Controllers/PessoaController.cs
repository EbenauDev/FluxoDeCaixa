using ControleDeCaixa.WebAPI.Handler;
using ControleDeCaixa.WebAPI.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaHandler _pessoaHandler;
        public PessoaController(IPessoaHandler pessoaHandler)
        {
            _pessoaHandler = pessoaHandler;
        }

        [HttpPost("Nova")]
        public async Task<IActionResult> AdicionarNovoCadastro([FromBody] PessoalInputModel inputModel)
        {
            if (await _pessoaHandler.NovaConta(inputModel) is var resultado && resultado.EhFalha)
                return BadRequest(resultado.Falha);
            return Ok(new
            {
                resultado.Sucesso.Avatar,
                resultado.Sucesso.Email,
                resultado.Sucesso.Username,
                token = ""
            });
        }

    }
}
