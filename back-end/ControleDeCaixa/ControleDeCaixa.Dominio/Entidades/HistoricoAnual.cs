using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.Dominio.Entidades
{
    public class HistoricoAnual
    {
        public HistoricoAnual(int id, int ano, DateTime dataDeCriacao, IEnumerable<HistoricoMes> movimentacoesMes)
        {
            Id = id;
            Ano = ano;
            DataDeCriacao = dataDeCriacao;
            MovimentacoesMes = movimentacoesMes;
        }

        public int Id { get; set; }
        public int Ano { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public IEnumerable<HistoricoMes> MovimentacoesMes { get; set; }
    }

    [Obsolete("Método obsoleto devido a mexida na classe OperacaoMes")]
    public class HistoricoMes
    {
        public HistoricoMes(int id, string mes, string descricao, IEnumerable<OperacaoMes> movimentacoes)
        {
            Id = id;
            Mes = mes;
            Descricao = descricao;
            Movimentacoes = movimentacoes;
            SaldoMovimentacoes = new Saldo(
                     totalReceitas: 0,
                     totalDespesas: 0
                );
        }

        public int Id { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Mes { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<OperacaoMes> Movimentacoes { get; set; }
        public Saldo SaldoMovimentacoes { get; set; }

    }

}
