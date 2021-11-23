using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.InputModels;
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
        Task<Resultado<Pessoa, Falha>> NovaConta(PessoalInputModel inputModel);
    }

    public class PessoaHandler : IPessoaHandler
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;
        private readonly ICriptografiaMD5 _criptografiaMD5;
        public PessoaHandler(IPessoaRepositorio pessoaRepositorio,
                             ICriptografiaMD5 criptografiaMD5)
        {
            _pessoaRepositorio = pessoaRepositorio;
            _criptografiaMD5 = criptografiaMD5;
        }

        public async Task<Resultado<Pessoa, Falha>> NovaConta(PessoalInputModel inputModel)
        {
            try
            {
                var pessoa = new Pessoa(inputModel.Email,
                                        inputModel.Senha,
                                        inputModel.Avatar,
                                        inputModel.Username);
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
    }
}
