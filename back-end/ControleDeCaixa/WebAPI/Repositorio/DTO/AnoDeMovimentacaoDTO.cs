using System;

namespace ControleDeCaixa.WebAPI.Repositorio.DTO
{
    public class AnoDeMovimentacaoDTO
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }

    }
}
