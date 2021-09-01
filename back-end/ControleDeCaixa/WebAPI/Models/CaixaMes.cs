using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Models
{
    public class CaixaMes
    {
        public CaixaMes(int id, string mes, string mesReferencia, List<Custo> custos, List<Receita> receitas)
        {
            Id = id;
            Mes = mes;
            MesReferencia = mesReferencia;
            Custos = custos;
            Receitas = receitas;
        }

        public int Id { get; set; }
        public string Mes { get; set; }
        public string MesReferencia { get; set; }
        public List<Custo> Custos { get; protected set; }
        public List<Receita> Receitas { get; protected set; }


        public void NovoCusto(Custo custo)
        {
            Custos.Add(custo);
        }
        
        public void NovaReceita(Receita receita) {
            Receitas.Add(receita);
        }
    }
}
