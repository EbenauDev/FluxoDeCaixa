using System;

namespace ControleDeCaixa.Dominio.Entidades
{
    public class PessoaResumida
    {
        public PessoaResumida(int id, string nome, DateTime dataNascimento, string avatar, string username, string email)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento.ToString("dd/MM/yyyy");
            Avatar = avatar;
            Username = username;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public static PessoaResumida ConverterParaPessoaResumida(Pessoa pessoa)
            => new PessoaResumida(
                pessoa.Id,
                pessoa.Nome,
                pessoa.DataNascimento,
                pessoa.Avatar,
                pessoa.Username,
                pessoa.Email);
    }
}
