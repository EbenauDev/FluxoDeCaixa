using System;

namespace ControleDeCaixa.WebAPI.Generics
{
    public sealed class Falha
    {
        public Falha(int codigoErro, string mensagem, Exception exception)
        {
            CodigoErro = codigoErro;
            Mensagem = mensagem;
            Exception = exception;
        }

        public Falha(int codigoErro, string mensagem)
        {
            CodigoErro = codigoErro;
            Mensagem = mensagem;
        }

        public static Falha Nova(int codigoErro, string mensagem)
            => new Falha(codigoErro, mensagem);

        public static Falha NovaComException(int codigoErro, string mensagem, Exception exception)
            => new Falha(codigoErro, mensagem, exception);

        public int CodigoErro { get; }
        public string Mensagem { get; }
        public Exception Exception { get; }
    }
}
