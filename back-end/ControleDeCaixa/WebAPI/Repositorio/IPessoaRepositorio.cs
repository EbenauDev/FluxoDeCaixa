using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IPessoaRepositorio
    {
        Task<Resultado<Pessoa, Falha>> NovaPessoaAsync(Pessoa pessoa);
    }
}
