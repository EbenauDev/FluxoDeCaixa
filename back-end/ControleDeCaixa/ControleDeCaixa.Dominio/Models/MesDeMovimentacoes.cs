using System;

namespace ControleDeCaixa.Dominio.Models
{
    public class MesDeMovimentacoes
    {
        public int IdAnoMovimentacoes { get; set; }
        public string Descricao { get; set; }
        public DateTime MesDeReferencia { get; set; }
    }
}
