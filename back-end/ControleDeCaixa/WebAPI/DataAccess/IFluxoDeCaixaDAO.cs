using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.InputModel;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.DataAccess
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<Resultado<int, Falha>> NovoFluxoAnualDeCaixaAsync(FluxoCaixaAnualInputModel fluxoCaixaAnualInput);
        Task<Resultado<int, Falha>> NovoCaixaAsync(Caixa caixa);
    }
}
