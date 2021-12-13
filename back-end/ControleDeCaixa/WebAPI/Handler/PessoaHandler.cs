using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Models;
using ContaPessoa = ControleDeCaixa.WebAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Repositorio;
using ControleDeCaixa.WebAPI.Aplicacao;
using ControleDeCaixa.WebAPI.Entidades;

namespace ControleDeCaixa.WebAPI.Handler
{

    public interface IPessoaHandler
    {
        Task<Resultado<Pessoa, Falha>> NovaContaAsync(PessoaInputModel inputModel);
        Task<Resultado<Pessoa, Falha>> AtualizarContaAsync(int pessoaId, PessoaAtualizada inputModel);
        Task<Resultado<Pessoa, Falha>> AtualizarSenhaAsync(int pessoaId, AlterarSenha alterarSenha);
    }

    public sealed class PessoaHandler : IPessoaHandler
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;
        private readonly ICriptografiaMD5 _criptografiaMD5;
        public PessoaHandler(IPessoaRepositorio pessoaRepositorio,
                             ICriptografiaMD5 criptografiaMD5)
        {
            _pessoaRepositorio = pessoaRepositorio;
            _criptografiaMD5 = criptografiaMD5;
        }

        public async Task<Resultado<Pessoa, Falha>> NovaContaAsync(PessoaInputModel inputModel)
        {
            try
            {
                var pessoa = new Pessoa(inputModel.Nome,
                                        inputModel.DataNascimento,
                                        inputModel.Avatar,
                                        inputModel.Senha,
                                        inputModel.Username,
                                        inputModel.Email);
                pessoa.AtualizarSenha(_criptografiaMD5.ConverterParaMD5(pessoa.Senha));
                if (await _pessoaRepositorio.NovaPessoaAsync(pessoa) is var resultado && resultado.EhFalha)
                    return resultado.Falha;
                return resultado.Sucesso;
            }
            catch (Exception ex)
            {
                return Falha.NovaComException("Houve um problema ao criar nova conta. Tente novamente mais tarde", ex);
            }

        }

        public async Task<Resultado<Pessoa, Falha>> AtualizarContaAsync(int pessoaId, PessoaAtualizada inputModel)
        {
            if (await _pessoaRepositorio.RecuperarPessoaPorIdAsync(pessoaId) is var pessoa && pessoa.EhFalha)
                return pessoa.Falha;
            pessoa.Sucesso
                .AtualizarAvatar(inputModel.Avatar)
                .AtualizarEmail(inputModel.Email);
            if (await _pessoaRepositorio.AtualizarPessoaAsync(pessoa.Sucesso) is var resultadoPessoa && resultadoPessoa.EhFalha)
                return resultadoPessoa.Falha;
            return resultadoPessoa.Sucesso;
        }

        public async Task<Resultado<Pessoa, Falha>> AtualizarSenhaAsync(int pessoaId, AlterarSenha alterarSenha)
        {
            if (alterarSenha.Senha.Equals(alterarSenha.ConfirmarSenha) == false)
                return Falha.Nova("As senhas não conferem");
            if (await _pessoaRepositorio.RecuperarPessoaPorIdAsync(pessoaId) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            var pessoa = resultado.Sucesso;
            resultado.Sucesso.AtualizarSenha(_criptografiaMD5.ConverterParaMD5(alterarSenha.Senha));
            if (await _pessoaRepositorio.AtualizarPessoaAsync(pessoa) is var resultadoPessoa && resultadoPessoa.EhFalha)
                return resultadoPessoa.Falha;
            return resultadoPessoa.Sucesso;
        }
    }
}
