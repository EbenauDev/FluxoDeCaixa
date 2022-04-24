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
        Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacaoAsync(MesDeMovimentacoes movimentacao, int pessoaId);
        Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacoesMes operacao, int pessoaId);
    }

    public class MovimentacoesHandler : IMovimentacoesHandler
    {
        private readonly IMovimentacoesRepositorio _movimentacoesRepositorio;
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        public MovimentacoesHandler(IMovimentacoesRepositorio movimentacoesRepositorio,
            IOperacoesRepositorio operacoesRepositorio)
        {
            _movimentacoesRepositorio = movimentacoesRepositorio;
            _operacoesRepositorio = operacoesRepositorio;
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

        public async Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacaoAsync(MesDeMovimentacoes movimentacao, int pessoaId)
        {
            var movimentacaoMes = new MovimentacaoMes(
                    movimentacao.IdAnoMovimentacoes,
                    movimentacao.MesDeReferencia,
                    movimentacao.Descricao);
            if (await _movimentacoesRepositorio.MesDeMovimentacaoJahExisteAsync(movimentacao.IdAnoMovimentacoes,
                                                                           movimentacao.MesDeReferencia.Month) is var resultadoValidacao && resultadoValidacao.Sucesso)
                return Falha.Nova("Já existe um mes de movimentações para o ano informado");
            if (await _movimentacoesRepositorio.NovoMesDeMovimentacoesAsync(movimentacaoMes) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            return resultado.Sucesso;
        }

        //TODO Verificar necessidade de ter esse handler
        public async Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacoesMes operacao, int pessoaId)
        {
            var movimentacaoMes = OperacaoMes.Nova(
                operacao.MesId,
                operacao.OperacaoTransacaoId,
                operacao.Valor,
                operacao.Descricao);
            if (await _operacoesRepositorio.NovaOperacaoNoMesAsync(movimentacaoMes) is var resultado && resultado.EhFalha)
                return resultado.Falha;
            return resultado.Sucesso;
        }
    }
}
