namespace ControleDeCaixa.WebAPI.Models
{
    public class Receita
    {
        public Receita(int id, string descricao, double valor)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }
}
