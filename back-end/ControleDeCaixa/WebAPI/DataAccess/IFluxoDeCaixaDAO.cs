using ControleDeCaixa.WebAPI.InputModel;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.DataAccess
{
    public interface IFluxoDeCaixaDAO
    {
        Task<int> NovoFluxoAnualDeCaixaAsync(FluxoCaixaAnualInputModel fluxoCaixaAnualInput);
        Task<int> NovoCaixaAsync(Caixa caixa);
    }
}
