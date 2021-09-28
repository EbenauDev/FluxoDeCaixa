namespace ControleDeCaixa.WebAPI.Generics
{
    public struct Resultado<TSucesso, TFalha>
    {
        public Resultado(TSucesso sucesso, TFalha falha)
        {
            Sucesso = sucesso;
            EhSucesso = default;
            Falha = falha;
            EhFalha = default;
        }

        internal Resultado(TSucesso sucesso)
        {
            Sucesso = sucesso;
            EhSucesso = true;
            Falha = default;
            EhFalha = default;
        }

        internal Resultado(TFalha falha)
        {
            Sucesso = default;
            EhSucesso = false;
            Falha = falha;
            EhFalha = true;
        }

        public static Resultado<TSucesso, TFalha> NovaFalha(TFalha falha)
            => new Resultado<TSucesso, TFalha>(default, falha);

        public static Resultado<TSucesso, TFalha> NovoSucesso(TSucesso sucesso)
            => new Resultado<TSucesso, TFalha>(sucesso, default);

        public TSucesso Sucesso { get; private set; }
        public bool EhSucesso { get; private set; }
        public TFalha Falha { get; private set; }
        public bool EhFalha { get; private set; }

    }
}


