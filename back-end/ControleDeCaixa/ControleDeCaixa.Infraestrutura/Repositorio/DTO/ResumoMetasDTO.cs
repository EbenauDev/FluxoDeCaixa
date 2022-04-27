using ControleDeCaixa.Dominio.Views;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.Infraestrutura.Repositorio.DTO
{
    public sealed class ResumoMetasDTO
    {
        public int Id { get; set; }
        public double ValorDesejado { get; set; }
        public string Descricao { get; set; }
        public double Saldo { get; set; }
        public double ValorRestante { get; set; }
        public string ProgressoMeta { get; set; }
    }

    public static class ResumoMetasDTOExtensao
    {
        public static IEnumerable<ResumoMetasViewModel> ConverterParaViewModel(this IEnumerable<ResumoMetasDTO> resumoMetas)
        {
            if (resumoMetas.Any() == false)
                return new List<ResumoMetasViewModel>();
            return resumoMetas.Select(r => new ResumoMetasViewModel(r.Id, r.ValorDesejado, r.Descricao, r.Saldo, r.ValorRestante, r.ProgressoMeta));
        }
    }
}
