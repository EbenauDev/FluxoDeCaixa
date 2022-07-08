namespace ControleDeCaixa.Core.Compartilhado
{
    public sealed class Credenciais : IValueObject
    {
        public Credenciais(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }

        public string Usuario { get; }
        public string Senha { get; }

        public static Credenciais Novas(string usuario, string senha)
            => new Credenciais(usuario, senha);
    }
}
