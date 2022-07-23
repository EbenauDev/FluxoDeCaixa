using ControleDeCaixa.Aplicacao.Cliente;
using ControleDeCaixa.Aplicacao.Identidade;
using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Dominio;
using ControleDeCaixa.Infra.Cliente;
using ControleDeCaixa.Infra.Identidade;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ControleDeCaixa.Aplicacao.Testes.Cliente
{
    public class SalvarClienteCommandTestes
    {

        [Fact]
        public async Task Excutar_Model_Valida()
        {

            var _identidadeRepositorioMock = new Mock<IIdentidadeRepositorio>();
            _identidadeRepositorioMock.Setup(i => i.UsuarioEstahDisponivel("jaotiago")).ReturnsAsync(true);

            var identidadeServicoMock = new IndentidadeServico(_identidadeRepositorioMock.Object);

            var inputModel = new NovoClienteInputModel
            {
                Nome = "João Tiago",
                Email = "teste@gmail.com",
                Nascimento = DateTime.Parse("2000-06-24T00:00:00"),
                Usuario = "jaotiago",
                Senha = "123456"
            };

            var pessoaMock = new PessoaFisica(Guid.Parse("f6bcfbd3-1445-4fd5-8c0b-48f988d0e839"),
                                               inputModel.Nome,
                                               inputModel.Email,
                                               inputModel.Nascimento)
                                    .AtualizarCredenciais(new Credenciais("jaotiago", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"));

            var clienteRespositorioMock = new Mock<IClienteRepositorio>();
            clienteRespositorioMock.Setup(c => c.SalvarRegistroClienteAsync(pessoaMock)).ReturnsAsync(pessoaMock);

            var comando = new SalvarRegistroClienteCommand(clienteRespositorioMock.Object,
                                                           new Mock<NullLoggerFactory>().Object,
                                                           identidadeServicoMock);

            var resultado = await comando.ExecutarAsync(inputModel);

            Assert.True(resultado.EhSucesso);
        }
    }
}
