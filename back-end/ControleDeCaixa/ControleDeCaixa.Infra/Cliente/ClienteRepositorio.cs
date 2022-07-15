using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Core.Utils;
using ControleDeCaixa.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Cliente
{
    public sealed class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _stringConexao;
        public ClienteRepositorio(IConnectionStringHelper _helper)
        {
            _stringConexao = _helper.GetConnection();
        }

        public async Task<Resultado<PessoaFisica, Falha>> SalvarRegistroClienteAsync(PessoaFisica pessoaFisica)
        {


            return Falha.Nova("Método de salvar cliente não foi implementado");
        }
    }
}
