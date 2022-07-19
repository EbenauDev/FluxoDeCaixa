using ControleDeCaixa.Aplicacao.Cliente;
using ControleDeCaixa.Infra.Cliente;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
            _command = new SalvarRegistroClienteCommand(new Mock<IClienteRepositorio>().Object, 
                                                        new Mock<NullLoggerFactory>().Object);
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

        //[Fact]
        //public async Task Excutar_Model_Valida()
        //{
        //    var resultado = await _command.ExecutarAsync(new NovoClienteInputModel
        //    {
        //        Nome = "João Tiago",
        //        Nascimento = DateTime.Parse("2000-06-24T00:00:00"),
        //        Usuario = "jaotiago",
        //        Senha = "123456"
        //    });

        //    Assert.True(resultado.EhSucesso);
        //}

        [Fact]
        public async Task Response_Model_Nao_Eh_Exception()
        {
            try
            {
                var resultado = await _command.ExecutarAsync(new NovoClienteInputModel
                {
                    Nome = "João Tiago",
                    Nascimento = DateTime.Parse("2000-06-24T00:00:00"),
                    Usuario = "jaotiago",
                    Senha = "123456"
                });

                Assert.IsType<RegistroClienteViewModel>(resultado);
            }
            catch (Exception e)
            {
                Assert.IsType<RegistroClienteViewModel>(e);
            }
        }
    }
}
