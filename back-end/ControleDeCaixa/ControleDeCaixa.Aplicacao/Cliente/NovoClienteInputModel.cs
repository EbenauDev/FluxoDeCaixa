using System;

namespace ControleDeCaixa.Aplicacao.Cliente
{
    public class NovoClienteInputModel
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
