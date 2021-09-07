using ControleDeCaixa.WebAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.WebAPI.DTO
{
    public class FluxoDeCaixaDTO
    {
        public int CaixaAnualId { get; set; }
        public int Ano { get; set; }
        public int CaixaMesId { get; set; }
        public string MesReferencia { get; set; }
        public int ReceitaId { get; set; }
        public int ValorReceita { get; set; }
        public string DescricaoReceita { get; set; }
        public int CustoId { get; set; }
        public int ValorCusto { get; set; }
        public string DescricaoCusto { get; set; }
    }

    public static class FluxoDeCaixaDTOExtensao
    {
        public static FluxoDeCaixaAnual ConverterParaModel(this IEnumerable<FluxoDeCaixaDTO> fluxoDeCaixaDTOs)
        {
            var fluxoCaixa = fluxoDeCaixaDTOs.Select(f => new { f.CaixaAnualId, f.Ano }).FirstOrDefault();
            var listaCaixas = fluxoDeCaixaDTOs.Select(f => new { f.CaixaMesId, f.MesReferencia });
            var receitas = fluxoDeCaixaDTOs.Select(f => new { f.ReceitaId, f.DescricaoReceita, f.ValorReceita, f.CaixaMesId });
            var custos = fluxoDeCaixaDTOs.Select(f => new { f.CustoId, f.DescricaoCusto, f.ValorCusto, f.CaixaMesId });

            return new FluxoDeCaixaAnual(fluxoCaixa.CaixaAnualId,
                                         fluxoCaixa.Ano,
                                         caixas: listaCaixas.Select(l => new CaixaDoMes(l.CaixaMesId,
                                                                l.MesReferencia,
                                                                receitas: receitas.Where(r => r.CaixaMesId.Equals(l.CaixaMesId)).Select(r => new Receita(r.ReceitaId, r.DescricaoReceita, r.ValorReceita)),
                                                                custos: custos.Where(c => c.CaixaMesId.Equals(l.CaixaMesId)).Select(c => new Custo(c.CustoId, c.DescricaoCusto, c.ValorCusto)),
                                                                historico: null)));
        }
    }
}

