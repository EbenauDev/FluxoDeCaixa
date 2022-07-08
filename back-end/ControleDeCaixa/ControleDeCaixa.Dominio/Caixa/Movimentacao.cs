using ControleDeCaixa.Core;
using ControleDeCaixa.Core.Compartilhado;
using System;

namespace ControleDeCaixa.Dominio
{
    public class Movimentacao : Entity
    {
        public DateTime DataDeMovimentacao { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
    }
}
