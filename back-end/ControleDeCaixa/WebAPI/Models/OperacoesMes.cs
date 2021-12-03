namespace ControleDeCaixa.WebAPI.Models
{
    public class OperacoesMes
    {
        public double Valor { get; set; }
        public int MesId { get; set; }
        public string Descricao { get; set; }
        public char TipoOperacao { get; set; }
    }
}