namespace ControleDeCaixa.Core.Compartilhado
{
    public sealed class Resultado<TSucesso, TFalha>
    {
        private Resultado(TSucesso sucesso)
        {
            Sucesso = sucesso;
            EhSucesso = true;
        }

        private Resultado(TFalha falha)
        {
            Falha = falha;
            EhFalha = true;
        }

        public TSucesso Sucesso { get; private set; }
        public bool EhSucesso { get; private set; }
        public TFalha Falha { get; private set; }
        public bool EhFalha { get; private set; }

        public static implicit operator Resultado<TSucesso, TFalha>(TSucesso sucesso)
                => new Resultado<TSucesso, TFalha>(sucesso);

        public static implicit operator Resultado<TSucesso, TFalha>(TFalha falha)
              => new Resultado<TSucesso, TFalha>(falha);
    }
}
