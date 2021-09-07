using System;
using System.Collections.Generic;

namespace ControleDeCaixa.WebAPI.Models
{
    public class FluxoDeCaixaAnual
    {
        public FluxoDeCaixaAnual(int id, int ano, IEnumerable<CaixaDoMes> caixas)
        {
            Id = id;
            Ano = ano;
            Caixas = caixas;
        }
        
        public int Id { get; set; }
        public int Ano { get; set; }
        public IEnumerable<CaixaDoMes> Caixas { get; set; }
    }
}
