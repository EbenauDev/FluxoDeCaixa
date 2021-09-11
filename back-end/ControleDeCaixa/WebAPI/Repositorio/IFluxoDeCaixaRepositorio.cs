using ControleDeCaixa.WebAPI.Entities;
using ControleDeCaixa.WebAPI.InputModel;
using ControleDeCaixa.WebAPI.Models;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<int> NovoFluxoAnualDeCaixaAsync(FluxoCaixaAnualInputModel fluxoCaixaAnualInput);
    }
}
