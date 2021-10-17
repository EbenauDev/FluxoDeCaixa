namespace ControleDeCaixa.WebAPI.ViewModels
{
    public class HistoricoCaixaViewModel
    {
        public HistoricoCaixaViewModel()
        {

        }

        public HistoricoCaixaViewModel(double totalReceitas, double totalCustos, int caixaMesId)
        {
            TotalReceitas = totalReceitas;
            TotalCustos = totalCustos;
            CaixaMesId = caixaMesId;
        }

        public double TotalReceitas { get; set; }
        public double TotalCustos { get; set; }
        public int CaixaMesId { get; set; }

        public void AtualizarReceitas(double receitas) => TotalReceitas += receitas;
        public void AtualizarCustos(double custos) => TotalCustos += custos;
    }
}
