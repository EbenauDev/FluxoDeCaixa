using System;

namespace ControleDeCaixa.WebAPI.Entidades
{
    public class MovimentacaoAnual
    {
        public MovimentacaoAnual(int id,
                                 int ano,
                                 string descricao,
                                 int pessoaId,
                                 DateTime dataDeCriacao)
        {
            Id = id;
            Ano = ano;
            Descricao = descricao;
            PessoaId = pessoaId;
            DataDeCriacao = dataDeCriacao;
        }

        public MovimentacaoAnual(int ano,
                                 string descricao,
                                 int pessoaId,
                                 DateTime dataDeCriacao)
        {
            Id = 0;
            Ano = ano;
            Descricao = descricao;
            PessoaId = pessoaId;
            DataDeCriacao = dataDeCriacao;
        }

        public int Id { get; set; }
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataDeCriacao { get; set; }

        public MovimentacaoAnual DefinirId(int id)
        {
            Id = id;
            return this;
        }
    }
}
