namespace ControleDeCaixa.Dominio.Entidades
{
    public class OperacaoMes
    {
        public OperacaoMes(int id, int mesId, int operacaoTransacaoId, decimal valor, string descricao)
        {
            Id = id;
            MesId = mesId;
            OperacaoTransacaoId = operacaoTransacaoId;
            Valor = valor;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public int MesId { get; set; }
        public int OperacaoTransacaoId { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public static OperacaoMes Nova(int mesId, int operacaoTransacaoId, decimal valor, string descricao)
            => new OperacaoMes(id: -1, mesId, operacaoTransacaoId, valor, descricao);

        public OperacaoMes DefinirId(int id)
        {
            Id = id;
            return this;
        }

        public OperacaoMes DefinirTipoTransacaoId(int operacaoTransacaoId)
        {
            OperacaoTransacaoId = operacaoTransacaoId;
            return this;
        }
    }

    public enum ETipoOperacaoMes
    {
        Saida = 'S',
        Entrada = 'E'
    }

}
