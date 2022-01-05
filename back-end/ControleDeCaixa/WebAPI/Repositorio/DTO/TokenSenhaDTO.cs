using System;

namespace ControleDeCaixa.WebAPI.Repositorio.DTO
{
    public sealed class TokenSenhaDTO
    {
        public Guid Id { get; set; }
        public DateTime ExpiraEm { get; set; }
        public DateTime GeradoEm { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public int PessoaId { get; set; }
        public bool JahUtilizacao { get; set; }
        public DateTime DataUtilizacao { get; set; }
    }
}
