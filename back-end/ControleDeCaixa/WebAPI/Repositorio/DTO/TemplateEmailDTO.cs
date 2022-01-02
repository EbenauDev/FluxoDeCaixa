namespace ControleDeCaixa.WebAPI.Repositorio.DTO
{
    public class TemplateEmailDTO
    {
        public int Id { get; set; }
        public string TipoDeUso { get; set; }
        public string HTML { get; set; }
        public string Assunto { get; set; }
        public string EnderecoOrigem { get; set; }
    }
}
