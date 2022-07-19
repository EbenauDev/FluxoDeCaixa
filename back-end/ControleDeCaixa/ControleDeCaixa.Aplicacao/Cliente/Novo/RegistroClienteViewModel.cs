using System;

namespace ControleDeCaixa.Aplicacao.Cliente
{
    public sealed class RegistroClienteViewModel
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }
    }
}
