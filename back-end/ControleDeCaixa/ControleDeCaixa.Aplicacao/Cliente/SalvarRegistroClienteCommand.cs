using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using ControleDeCaixa.Infra.Cliente;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Aplicacao.Cliente
{

    public interface ISalvarRegistroClienteCommand
    {
        Task<Resultado<bool, Falha>> ExecutarAsync(NovoClienteInputModel inputModel);
    }

    public sealed class SalvarRegistroClienteCommand : ISalvarRegistroClienteCommand
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public SalvarRegistroClienteCommand(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<Resultado<bool, Falha>> ExecutarAsync(NovoClienteInputModel inputModel)
        {
            var resultadoValidacao = new NovoClienteInputModelValidator().Validate(inputModel);
            if (resultadoValidacao.IsValid is false)
                return Falha.Nova(resultadoValidacao.Errors.ConvertAll(e => $"{e.ErrorCode} - {e.ErrorMessage}"));

            //Criar entidade
            var pessoa = PessoaFisica.Nova(inputModel.Nome,
                                           inputModel.Nascimento,
                                           new Credenciais(inputModel.Usuario, inputModel.Senha));
            //Persistir no banco
            await _clienteRepositorio.SalvarRegistroCliente(pessoa);
            return true;
        }
    }
}
