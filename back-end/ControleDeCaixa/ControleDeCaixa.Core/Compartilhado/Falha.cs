using System.Collections.Generic;
using System.Linq;

namespace ControleDeCaixa.Core.Compartilhado
{
    public class Falha
    {
        public Falha()
        {
            Mensagens = new List<string>();
        }

        public Falha(string mensagem)
        {
            Mensagens.Add(mensagem);
        }

        public List<string> Mensagens { get; private set; }

        public Falha AdicionarMensagem(string mensagem)
        {
            Mensagens.Add(mensagem);
            return this;
        }

        public Falha AdicionarMensagens(IEnumerable<string> mensagem)
        {
            Mensagens.AddRange(mensagem);
            return this;
        }

        public static Falha Nova(string mensagem)
            => new Falha(mensagem);

        public static Falha Nova(IEnumerable<string> mensagem)
            => new Falha().AdicionarMensagens(mensagem);

        public bool PossuiMensagens()
            => Mensagens.Any();
    }
}
