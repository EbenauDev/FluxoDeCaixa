using System;

namespace ControleDeCaixa.Infraestrutura.Repositorio.DTO
{
    public class AnoDeMovimentacaoDTO
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }

    }
}
