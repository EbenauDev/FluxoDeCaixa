using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IPessoaRepositorio
    {
        Task<Resultado<Pessoa, Falha>> NovaPessoaAsync(Pessoa pessoa);
        Task<Resultado<Pessoa, Falha>> AtualizarPessoaAsync(Pessoa pessoa);
        Task<Resultado<Pessoa, Falha>> RecuperarPessoaPorIdAsync(int pessoaId);
        Task<Resultado<bool, Falha>> UsernameEstahDisponivelAsync(string username);
    }
}
