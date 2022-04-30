using ControleDeCaixa.Dominio.Entidades;
using ControleDeCaixa.Dominio.Models;
using ControleDeCaixa.Infraestrutura.Repositorio;
using ControleDeCaixa.Infraestrutura.Servicos;
using ControleDeCaixa.Testes.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControleDeCaixa.Testes.Repositorio
{
    public class PessoaRepositorioTestes
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;
        private readonly CriptografiaMD5 _cripto;
        public PessoaRepositorioTestes()
        {
            var config = new ConnectionHelper();
            _pessoaRepositorio = new PessoaRepositorio(config);
            _cripto = new CriptografiaMD5();
        }


        [Fact]
        public async Task Criar_Nova_Pessoa()
        {
            var pessoa = new Pessoa(id: 0,
                                    nome: "João dos Testes",
                                    dataNascimento: DateTime.Parse("2000-06-24T00:00:00"),
                                    avatar: "",
                                    senha: _cripto.ConverterParaMD5("123456"),
                                    username: "JuaoDosTestes",
                                    email: "juaodosteste@email.com");

            var resultado = await _pessoaRepositorio.NovaPessoaAsync(pessoa);
            Assert.True(resultado.EhSucesso);
        }
    }
}
