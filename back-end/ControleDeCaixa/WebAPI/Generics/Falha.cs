using System;

namespace ControleDeCaixa.WebAPI.Generics
{
    public sealed class Falha
    {
        public Falha(string mensagem, Exception exception)
        {
            Mensagem = mensagem;
            Exception = exception;
        }

        public Falha(string mensagem)
        {
            Mensagem = mensagem;
        }

        public static Falha Nova(string mensagem)
            => new Falha(mensagem);


        public static Falha NovaComException(string mensagem, Exception exception)
            => new Falha(mensagem, exception);

        public string Mensagem { get; }
        public Exception Exception { get; }
    }
}
