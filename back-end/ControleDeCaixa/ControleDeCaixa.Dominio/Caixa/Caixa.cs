using ControleDeCaixa.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeCaixa.Dominio
{
    public sealed class Caixa : Entity, IAgreggateRoot
    {
        public string Codigo { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public IEnumerable<Movimentacao> Movimentacoes { get; set; }
        public SituacaoCaixa Situacao { get; set; }
    }

    public enum SituacaoCaixa
    {
        Aberto = 'A',
        Fechado = 'F'
    }
}
