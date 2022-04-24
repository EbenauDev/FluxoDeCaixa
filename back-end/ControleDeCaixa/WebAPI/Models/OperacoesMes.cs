namespace ControleDeCaixa.WebAPI.Models
{
    public class OperacoesMes
    {
        public int MesId { get; set; }
        public int OperacaoTransacaoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}