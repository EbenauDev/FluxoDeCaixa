using System;

namespace ControleDeCaixa.WebAPI.Models
{
    public class MesDeMovimentacoes
    {
        public int IdAnoMovimentacoes { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Descricao { get; set; }
    }
}
