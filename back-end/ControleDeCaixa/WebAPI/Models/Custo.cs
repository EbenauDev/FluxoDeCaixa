using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Models
{
    public class Custo
    {
        public Custo(int id, string descricao, double valor)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }
}
