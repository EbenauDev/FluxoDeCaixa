using ControleDeCaixa.WebAPI.Entities;
using ControleDeCaixa.WebAPI.Models;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IFluxoDeCaixaRepositorio
    {
        Task<FluxoDeCaixaAnual> RecuperarFluxoDeCaixaAsync(string ano);
        Task<bool> NovoCustoAsync(CustoInputModel custo);
        Task<bool> NovaReceitaAsync(ReceitaInputModel receita);
    }
}
