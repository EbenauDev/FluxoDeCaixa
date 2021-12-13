using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.WebAPI.Entidades
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

    public class HistoricoMes
    {
        public HistoricoMes(int id, DateTime mesDeReferencia, string descricao, IEnumerable<OperacaoMes> movimentacoes)
        {
            Id = id;
            MesDeReferencia = mesDeReferencia;
            Mes = mesDeReferencia.ToString("MMMM");
            Descricao = descricao;
            Movimentacoes = movimentacoes;
            SaldoMovimentacoes = new Saldo(
                     totalReceitas: movimentacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Entrada).Sum(o => o.Valor),
                     totalDespesas: movimentacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Saida).Sum(o => o.Valor)
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
