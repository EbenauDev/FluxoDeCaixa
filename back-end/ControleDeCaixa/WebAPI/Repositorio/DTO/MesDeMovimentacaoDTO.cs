using System;
using System.Globalization;

namespace ControleDeCaixa.WebAPI.Repositorio.DTO
{
    public class MesDeMovimentacaoDTO
    {
        public int Id { get; set; }
        public DateTime MesDeReferencia { get; set; }
        public string Mes
        {
            get => MesDeReferencia.ToString("MMMM", CultureInfo.GetCultureInfo("pt-BR"));
            set => Mes = value;
        }
        public DateTime DataDeCriacao { get; set; }
    }
}
