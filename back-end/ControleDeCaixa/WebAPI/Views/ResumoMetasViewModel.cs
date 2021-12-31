namespace ControleDeCaixa.WebAPI.Views
{
    public sealed class ResumoMetasViewModel
    {
        public ResumoMetasViewModel(int id, double valorDesejado, string descricao, double saldo, double valorRestante, string progressoMeta)
        {
            Id = id;
            ValorDesejado = valorDesejado;
            Descricao = descricao;
            Saldo = saldo;
            ValorRestante = valorRestante;
            ProgressoMeta = progressoMeta;
        }

        public int Id { get; set; }
        public double ValorDesejado { get; set; }
        public string Descricao { get; set; }
        public double Saldo { get; set; }
        public double ValorRestante { get; set; }
        public string ProgressoMeta { get; set; }
    }
}
