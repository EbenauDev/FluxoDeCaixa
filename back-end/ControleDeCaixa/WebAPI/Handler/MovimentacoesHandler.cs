using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio;
using System;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Handler
{

    public interface IMovimentacoesHandler
    {
        Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacoesAsync(MoviementacoesAnuais moviementacoes, int pessoaId);
        Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacao(MesDeMovimentacoes movimentacao, int pessoaId);
        Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacoesMes operacoes, int pessoaId);
    }

    public class MovimentacoesHandler : IMovimentacoesHandler
    {
        private readonly IMovimentacoesRepositorio _movimentacoesRepositorio;
        public MovimentacoesHandler(IMovimentacoesRepositorio movimentacoesRepositorio)
        {
            _movimentacoesRepositorio = movimentacoesRepositorio;
        }

        public async Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacoesAsync(MoviementacoesAnuais moviementacoes, int pessoaId)
        {
            if (await _movimentacoesRepositorio.AnoDeMovimentacoesJahExiste(moviementacoes.Ano, pessoaId) is var anoJahExiste && anoJahExiste.EhFalha)
                return anoJahExiste.Falha;
            if (anoJahExiste.Sucesso)
                return Falha.Nova($"Já existe um registro para o ano {moviementacoes.Ano}");
            var anoDeMovimentacao = new MovimentacaoAnual(
                    moviementacoes.Ano,
                    moviementacoes.Descricao,
                    pessoaId,
                    dataDeCriacao: DateTime.Now
                );
            if (await _movimentacoesRepositorio.NovoAnoDeMovimentacaoAsync(anoDeMovimentacao) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            return resultado.Sucesso;
        }

        public async Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacao(MesDeMovimentacoes movimentacao, int pessoaId)
        {
            var movimentacaoMes = new MovimentacaoMes(
                    movimentacao.IdAnoMovimentacoes,
                    movimentacao.MesDeReferencia,
                    movimentacao.Descricao
                );
            if (await _movimentacoesRepositorio.NovoMesDeMovimentacoesAsync(movimentacaoMes) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            return resultado.Sucesso;
        }

        public async Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacoesMes operacoes, int pessoaId)
        {
    
            var movimentacaoMes = new OperacaoMes(
                operacoes.Valor,
                operacoes.MesId,
                operacoes.Descricao,
                (EMovimentacaoMes)operacoes.TipoOperacao
               );
            if (await _movimentacoesRepositorio.NovaOperacaoNoMesAsync(movimentacaoMes) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            return resultado.Sucesso;
        }
    }
}
