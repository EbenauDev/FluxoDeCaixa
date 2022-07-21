using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using ControleDeCaixa.Dominio.ServicosDeDominio.Identidade;
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
        private readonly IIndentidadeServico _indentidadeServico;
        private readonly ILogger _logger;

        public SalvarRegistroClienteCommand(IClienteRepositorio clienteRepositorio,
                                            ILoggerFactory loggerFactory,
                                            IIndentidadeServico indentidadeServico)
        {
            _clienteRepositorio = clienteRepositorio;
            _indentidadeServico = indentidadeServico;
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

                var credenciais = await _indentidadeServico.CriarCredenciaisAsync(inputModel.Usuario, inputModel.Senha);
                if (credenciais.EhSucesso is false)
                    return credenciais.Falha;

                pessoa.AtualizarCredenciais(credenciais.Sucesso);

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
