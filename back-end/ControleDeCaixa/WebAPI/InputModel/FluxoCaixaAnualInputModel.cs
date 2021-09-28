using System.ComponentModel.DataAnnotations;

namespace ControleDeCaixa.WebAPI.InputModel
{
    public class FluxoCaixaAnualInputModel
    {
        public int Ano { get; set; }
        public string Descricao { get; set; }
    }
}
