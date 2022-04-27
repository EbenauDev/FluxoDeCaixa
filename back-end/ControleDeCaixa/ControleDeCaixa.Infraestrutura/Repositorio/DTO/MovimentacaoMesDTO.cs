using ControleDeCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.Infraestrutura.Repositorio.DTO
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
        public int MesId { get; set; }
        public int OperacaoTransacaoId { get; set; }
        public decimal Valor { get; set; }
        public string OperacoesDoMesDescricao { get; set; }
        public char TipoOperacao { get; set; }

    }

    public static class MovimentacaoMesDTOExtensao
    {
        public static MovimentacaoMes ConverterParaEntidade(this IEnumerable<MovimentacaoMesDTO> movimentacaoMesDTOs)
        {
            var movimentacaoMes = movimentacaoMesDTOs.Select(m => new MovimentacaoMes(m.Id, m.IdAnoMovimentacoes, m.MesDeReferencia, m.Descricao)).FirstOrDefault();
            var operacoes = movimentacaoMesDTOs.Select(m => new OperacaoMes(m.OperacoesDoMesId, m.MesId, m.OperacaoTransacaoId, m.Valor,  m.OperacoesDoMesDescricao));
            if (operacoes.Any())
            {
                movimentacaoMes.DefinirDespesas(operacoes.Where(o => o.OperacaoTransacaoId == 4));
                movimentacaoMes.DefinirReceitas(operacoes.Where(o => o.OperacaoTransacaoId == 1));
                movimentacaoMes.DefinirSaldo(new Saldo(
                    totalReceitas: operacoes.Where(o => o.OperacaoTransacaoId == 4).Sum(o => o.Valor),
                    totalDespesas: operacoes.Where(o => o.OperacaoTransacaoId == 1).Sum(o => o.Valor)
                    ));
            }

            return movimentacaoMes;
        }
    }
}

