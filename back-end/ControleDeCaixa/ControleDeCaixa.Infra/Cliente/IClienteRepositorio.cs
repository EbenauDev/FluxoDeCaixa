using ControleDeCaixa.Dominio;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Cliente
{
    public interface IClienteRepositorio
    {
        Task SalvarRegistroCliente(PessoaFisica pessoaFisica);
    }
}
