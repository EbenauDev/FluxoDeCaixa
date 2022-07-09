using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Aplicacao.Cliente
{
    public sealed class SalvarRegistroClienteCommand : ICommand<NovoClienteInputModel>
    {
        public SalvarRegistroClienteCommand(NovoClienteInputModel payload)
        {
            Payload = payload;
        }

        public NovoClienteInputModel Payload { get; }

        public async Task ExecutarAsync()
        {
            //Validar usuário

            //Criar entidade
            var nova = PessoaFisica.Nova(Payload.Nome, Payload.Nascimento, new Credenciais(Payload.Usuario, Payload.Senha));
            return;
            //Persistir no banco
        }
    }
}
