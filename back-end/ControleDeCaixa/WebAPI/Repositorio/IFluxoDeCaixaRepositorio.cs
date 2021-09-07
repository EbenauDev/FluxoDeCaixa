using ControleDeCaixa.WebAPI.Models;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<FluxoDeCaixaAnual> RecuperarFluxoDeCaixa(string ano);
    }
}
