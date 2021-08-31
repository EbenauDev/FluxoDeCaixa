using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Models
{
    public class FluxoCaixa
    {
        public string AnoReferencia { get; set; }
        public IEnumerable<CaixaMes> CaixaDoMes { get; set; }
    }
}
