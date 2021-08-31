using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Models
{
    public class CaixaMes
    {
        public int Id { get; set; }
        public string Mes { get; set; }
        public string MesReferencia { get; set; }
        public IEnumerable<Custo> Custos { get; set; }
        public IEnumerable<Receita> Receitas { get; set; }
    }
}
