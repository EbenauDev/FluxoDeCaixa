using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControleDeCaixa.WebAPI.Entidades
{
    public class OperacaoMes
    {
        public OperacaoMes(int id, double valor, int mesId, string descricao, ETipoOperacaoMes tipoOperacao)
        {
            Id = id;
            Valor = valor;
            MesId = mesId;
            Descricao = descricao;
            TipoOperacao = tipoOperacao;
        }

        public OperacaoMes(double valor, int mesId, string descricao, ETipoOperacaoMes tipoOperacao)
        {
            Valor = valor;
            MesId = mesId;
            Descricao = descricao;
            TipoOperacao = tipoOperacao;
        }

        public int Id { get; set; }
        public double Valor { get; set; }
        public int MesId { get; set; }
        public string Descricao { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ETipoOperacaoMes TipoOperacao { get; set; }

        public OperacaoMes DefinirId(int id)
        {
            Id = id;
            return this;
        }
    }

    public enum ETipoOperacaoMes
    {
        Saida = 'S',
        Entrada = 'E'
    }

}
