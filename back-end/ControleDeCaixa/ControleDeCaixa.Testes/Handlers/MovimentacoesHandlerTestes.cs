using ControleDeCaixa.Aplicacao.Handler;
using ControleDeCaixa.Dominio.Models;
using ControleDeCaixa.Infraestrutura.Repositorio;
using ControleDeCaixa.Testes.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControleDeCaixa.Testes.Handlers
{
    public class MovimentacoesHandlerTestes
    {
        private readonly IMovimentacoesHandler _handler;
        public MovimentacoesHandlerTestes()
        {
            var config = new ConnectionHelper();
            _handler = new MovimentacoesHandler(new MovimentacoesRepositorio(config),
                                                new OperacoesRepositorio(config));
        }


        [Fact]
        public async Task Criar_Ano_Movimentacoes()
        {
            //1002
            var movimentacoes = new MoviementacoesAnuais
            {
                Ano = 2022,
                Descricao = "Controlar as movimentações feitas no ano de 2022"
            };
            var resultato = await _handler.NovoAnoDeMovimentacoesAsync(movimentacoes, pessoaId: 1002);
            Assert.True(resultato.EhSucesso);
        }
    }
}
