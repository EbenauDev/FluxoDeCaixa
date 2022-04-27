namespace ControleDeCaixa.Dominio.Views
{
    public class TemplateEmailViewModel
    {
        public TemplateEmailViewModel(int id, string tipoDeUso, string html, string assunto, string enderecoOrigem)
        {
            Id = id;
            TipoDeUso = tipoDeUso;
            Html = html;
            Assunto = assunto;
            EnderecoOrigem = enderecoOrigem;
        }

        public int Id { get; set; }
        public string TipoDeUso { get; set; }
        public string Html { get; set; }
        public string Assunto { get; set; }
        public string EnderecoOrigem { get; set; }
    }
}
