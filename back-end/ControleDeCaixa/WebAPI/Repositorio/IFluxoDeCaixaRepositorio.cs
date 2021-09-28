using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.InputModel;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<Resultado<bool, Falha>> IncluirOperacaoCaixaAsync(OperacaoCaixaInputModel operacaoCaixaInput);
    }
}
