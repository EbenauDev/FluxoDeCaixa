using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Aplicacao.Cliente
{

    public interface ISalvarRegistroClienteCommand
    {

    }

    public sealed class SalvarRegistroClienteCommand : ISalvarRegistroClienteCommand
    {

        public async Task ExecutarAsync(NovoClienteInputModel inputModel)
        {
            //Validar usuário

            //Criar entidade
            var nova = PessoaFisica.Nova(inputModel.Nome, inputModel.Nascimento, new Credenciais(inputModel.Usuario, inputModel.Senha));
            return;
            //Persistir no banco
        }
    }
}
