using System;

namespace ControleDeCaixa.Dominio.Entidades
{
    public sealed class TokenSenha
    {
        public TokenSenha(Guid id, DateTime? expiraEm, DateTime? geradoEm, DateTime dataDeRegistro, int pessoaId)
        {
            Id = id;
            ExpiraEm = expiraEm ?? DateTime.Parse("1753-01-01T00:00:00");
            GeradoEm = geradoEm ?? DateTime.Parse("1753-01-01T00:00:00");
            DataUtilizacao = DateTime.Parse("1753-01-01T00:00:00");
            DataDeRegistro = dataDeRegistro;
            PessoaId = pessoaId;
            JahUtilizado = false;
        }

        public Guid Id { get; set; }
        public DateTime ExpiraEm { get; set; }
        public DateTime GeradoEm { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public int PessoaId { get; set; }
        public bool JahUtilizado { get; set; }
        public DateTime DataUtilizacao { get; set; }

        public static TokenSenha GerarNovoToken(DateTime expiraEm, int pessoaId)
            => new TokenSenha(Guid.NewGuid(), expiraEm, DateTime.Now, DateTime.Now, pessoaId);

        public TokenSenha QueimarToken(bool jahUtilizado, DateTime dataUtilizacao)
        {
            this.JahUtilizado = jahUtilizado;
            this.DataUtilizacao = dataUtilizacao;
            return this;
        }
    }
}