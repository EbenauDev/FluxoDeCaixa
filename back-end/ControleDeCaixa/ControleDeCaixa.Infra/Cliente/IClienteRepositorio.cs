using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Cliente
{
    public interface IClienteRepositorio
    {
        Task<Resultado<PessoaFisica, Falha>> SalvarRegistroClienteAsync(PessoaFisica pessoaFisica);
    }
}
