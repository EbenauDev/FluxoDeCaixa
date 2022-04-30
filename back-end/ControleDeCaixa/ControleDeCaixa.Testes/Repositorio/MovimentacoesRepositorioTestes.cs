using ControleDeCaixa.Infraestrutura.Repositorio;
using ControleDeCaixa.Testes.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControleDeCaixa.Testes.Repositorio
{
    public class MovimentacoesRepositorioTestes
    {

        private IMovimentacoesRepositorio _movimentacoesRepositorio;
        public MovimentacoesRepositorioTestes()
        {
            var config = new ConnectionHelper();
            _movimentacoesRepositorio = new MovimentacoesRepositorio(config);
        }

        [Fact]
        public async Task Recuperar_Movimentacoes_Mes()
        {
            var resultado = await _movimentacoesRepositorio.ListarMovimentacoesDoMesAsync(anoId: 1, mesId: 1);
            Assert.True(resultado.EhSucesso);
        }

        [Fact]
        public async Task Criar_Ano_De_Movimentacoes() { 

        }

    }
}
