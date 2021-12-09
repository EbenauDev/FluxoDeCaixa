using System;

namespace ControleDeCaixa.WebAPI.Entidades
{
    public class Sessao
    {
        public Sessao(Guid id, int idPessoa, DateTime? dataDeLogin, DateTime? dataLogout)
        {
            Id = id;
            IdPessoa = idPessoa;
            DataDeLogin = dataDeLogin;
            DataLogout = dataLogout;
        }

        public Guid Id { get; set; }
        public int IdPessoa { get; set; }
        public DateTime? DataDeLogin { get; set; }
        public DateTime? DataLogout { get; set; }
    }
}
