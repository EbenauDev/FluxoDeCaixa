using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using ControleDeCaixa.Infra.Cliente;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Aplicacao.Cliente
{

    public interface ISalvarRegistroClienteCommand
    {
        Task<Resultado<RegistroClienteViewModel, Falha>> ExecutarAsync(NovoClienteInputModel inputModel);
    }

    public sealed class SalvarRegistroClienteCommand : ISalvarRegistroClienteCommand
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ILogger _logger;
        public SalvarRegistroClienteCommand(IClienteRepositorio clienteRepositorio, ILoggerFactory loggerFactory)
        {
            _clienteRepositorio = clienteRepositorio;
            _logger = loggerFactory.CreateLogger<SalvarRegistroClienteCommand>();
        }

        public async Task<Resultado<RegistroClienteViewModel, Falha>> ExecutarAsync(NovoClienteInputModel inputModel)
        {
            try
            {
                var resultadoValidacao = new NovoClienteInputModelValidator().Validate(inputModel);
                if (resultadoValidacao.IsValid is false)
                    return Falha.Nova(resultadoValidacao.Errors.ConvertAll(e => $"{e.PropertyName} - {e.ErrorMessage}"));

                var pessoa = PessoaFisica.Nova(inputModel.Nome,
                                               inputModel.Email,
                                               inputModel.Nascimento);

                //TODO:: Gerenciar as credenciais da pessoa


                //TODO:: Atualizar entidade cliente com as novas credenciais


                var cliente = await _clienteRepositorio.SalvarRegistroClienteAsync(pessoa);

                return new RegistroClienteViewModel
                {
                    Nome = cliente.Sucesso.Nome,
                    Nascimento = cliente.Sucesso.DataNascimento,
                    Usuario = cliente.Sucesso.Credenciais.Usuario,
                    Token = String.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Falha ao salvar registro do cliente. Exception: {exception}", ex);
                return Falha.Nova("Falha ao salvar o seu registro. por favor, tente novamente mais tarde");
            }
        }
    }
}
