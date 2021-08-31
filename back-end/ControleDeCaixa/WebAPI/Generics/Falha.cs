using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Generics
{
    public class Falha
    {
        public Falha(string mensagem)
        {
            Mensagem = mensagem;
        }

        public Falha(string mensagem, Exception exception)
        {
            Mensagem = mensagem;
            Excecao = exception;
        }

        public string Mensagem { get; set; }
        public Exception Excecao { get; set; }
        public Falha FalhaInterna { get; set; }


        public static Falha Nova(string mensagem)
        {
            return new Falha(mensagem);
        }
    }
}
