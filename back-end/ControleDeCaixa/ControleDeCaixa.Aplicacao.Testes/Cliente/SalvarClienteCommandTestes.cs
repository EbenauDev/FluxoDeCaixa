using ControleDeCaixa.Aplicacao.Cliente;
using ControleDeCaixa.Infra.Cliente;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControleDeCaixa.Aplicacao.Testes.Cliente
{
    public class SalvarClienteCommandTestes
    {
        private readonly SalvarRegistroClienteCommand _command;
        public SalvarClienteCommandTestes()
        {
            _command = new SalvarRegistroClienteCommand(new Mock<IClienteRepositorio>().Object);
        }

        [Fact]
        public async Task Excutar_Model_Invalida()
        {
            var resultado = await _command.ExecutarAsync(new NovoClienteInputModel
            {
                Nome = String.Empty,
                Nascimento = DateTime.MinValue,
                Usuario = "123",
                Senha = "12345"
            });

            Assert.False(resultado.EhSucesso);
        }

        [Fact]
        public async Task Excutar_Model_Valida()
        {
            var resultado = await _command.ExecutarAsync(new NovoClienteInputModel
            {
                Nome = "João Tiago",
                Nascimento = DateTime.Parse("2000-06-24T00:00:00"),
                Usuario = "jaotiago",
                Senha = "123456"
            });

            Assert.True(resultado.EhSucesso);
        }
    }
}
