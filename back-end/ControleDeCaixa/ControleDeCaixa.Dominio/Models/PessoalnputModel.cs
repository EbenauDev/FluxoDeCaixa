﻿using System;

namespace ControleDeCaixa.Dominio.Models
{
    public class PessoaInputModel
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
    }
}
