using ControleDeCaixa.WebAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.WebAPI.Repositorio.DTO
{
    public class MovimentacaoMesDTO
    {
        //MovimentacaoMes
        public int Id { get; set; }
        public int IdAnoMovimentacoes { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Descricao { get; set; }
        //OperacaoMes
        public int OperacoesDoMesId { get; set; }
        public double Valor { get; set; }
        public int MesId { get; set; }
        public string OperacoesDoMesDescricao { get; set; }
        public char TipoOperacao { get; set; }

    }

    public static class MovimentacaoMesDTOExtensao
    {
        public static MovimentacaoMes ConverterParaEntidade(this IEnumerable<MovimentacaoMesDTO> movimentacaoMesDTOs)
        {
            var movimentacaoMes = movimentacaoMesDTOs.Select(m => new MovimentacaoMes(m.Id, m.IdAnoMovimentacoes, m.MesDeReferencia, m.Descricao)).FirstOrDefault();
            var operacoes = movimentacaoMesDTOs.Select(m => new OperacaoMes(m.OperacoesDoMesId, m.Valor, m.MesId, m.OperacoesDoMesDescricao, (ETipoOperacaoMes)m.TipoOperacao));
            if (operacoes.Any())
            {
                movimentacaoMes.DefinirDespesas(operacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Saida));
                movimentacaoMes.DefinirReceitas(operacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Entrada));
                movimentacaoMes.DefinirSaldo(new Saldo(
                    totalReceitas: operacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Entrada).Sum(o => o.Valor),
                    totalDespesas: operacoes.Where(o => o.TipoOperacao == ETipoOperacaoMes.Saida).Sum(o => o.Valor)
                    ));
            }

            return movimentacaoMes;
        }
    }
}

