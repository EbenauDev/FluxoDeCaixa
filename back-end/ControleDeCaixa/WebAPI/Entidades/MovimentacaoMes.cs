using System;

namespace ControleDeCaixa.WebAPI.Models
{
    public class MovimentacaoMes
    {
        public MovimentacaoMes(int idAnoMovimentacoes, DateTime mesDeReferencia, string descricao)
        {
            Id = 0;
            IdAnoMovimentacoes = idAnoMovimentacoes;
            MesDeReferencia = mesDeReferencia;
            Descricao = descricao;
        }

        public MovimentacaoMes(int id, int idAnoMovimentacoes, DateTime mesDeReferencia, string descricao)
        {
            Id = id;
            IdAnoMovimentacoes = idAnoMovimentacoes;
            MesDeReferencia = mesDeReferencia;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public int IdAnoMovimentacoes { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Descricao { get; set; }

        public MovimentacaoMes DefinirId(int id)
        {
            Id = id;
            return this;
        }

    }


}
