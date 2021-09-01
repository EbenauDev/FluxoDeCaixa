using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Models
{
    public class FluxoCaixa
    {
        public FluxoCaixa(string anoReferencia, IEnumerable<CaixaMes> caixaDoMes)
        {
            AnoReferencia = anoReferencia;
            CaixaDoMes = caixaDoMes;
        }

        public string AnoReferencia { get; set; }
        public IEnumerable<CaixaMes> CaixaDoMes { get; set; }
    }
}
