using ControleDeCaixa.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Cliente
{
    public interface IClienteRepositorio
    {
        Task SalvarRegistroCliente(PessoaFisica pessoaFisica);
    }
}
