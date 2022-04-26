using System;
using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Entidades
{
    //TODO: Refatorar o método que popula essa classe
    public class MovimentacaoMes
    {
        public MovimentacaoMes(int idAnoMovimentacoes, DateTime mesDeReferencia, string descricao)
        {
            Id = 0;
            IdAnoMovimentacoes = idAnoMovimentacoes;
            MesDeReferencia = mesDeReferencia;
            Descricao = descricao;
        }

        public MovimentacaoMes(int id, int idAnoMovimentacoes, DateTime mesDeReferencia, string descricao)
        {
            Id = id;
            IdAnoMovimentacoes = idAnoMovimentacoes;
            MesDeReferencia = mesDeReferencia;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public int IdAnoMovimentacoes { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Descricao { get; set; }

        public IEnumerable<OperacaoMes> Receitas { get; set; } = new List<OperacaoMes>();
        public IEnumerable<OperacaoMes> Despesas { get; set; } = new List<OperacaoMes>();
        public Saldo SaldoDoMes { get; set; } = new Saldo();

        public MovimentacaoMes DefinirId(int id)
        {
            Id = id;
            return this;
        }

        public MovimentacaoMes DefinirReceitas(IEnumerable<OperacaoMes> receitas)
        {
            Receitas = receitas;
            return this;
        }

        public MovimentacaoMes DefinirDespesas(IEnumerable<OperacaoMes> despesas)
        {
            Despesas = despesas;
            return this;
        }

        public MovimentacaoMes DefinirSaldo(Saldo saldo)
        {
            SaldoDoMes = saldo;
            return this;
        }
    }

    [Obsolete]
    public sealed class Saldo
    {
        public Saldo(decimal totalReceitas = 0, decimal totalDespesas = 0)
        {
            TotalReceitas = totalReceitas;
            TotalDespesas = totalDespesas;
            SaldoTotal = totalReceitas - totalDespesas;
        }

        public decimal TotalReceitas { get; set; } = 0;
        public decimal TotalDespesas { get; set; } = 0;
        public decimal SaldoTotal { get; set; } = 0;

    }

}
