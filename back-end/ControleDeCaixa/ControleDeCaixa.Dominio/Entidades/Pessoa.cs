using System;

namespace ControleDeCaixa.Dominio.Entidades
{
    public class Pessoa
    {
        public Pessoa(int id, string nome, DateTime dataNascimento, string avatar, string senha, string username, string email)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Avatar = avatar;
            Senha = senha;
            Username = username;
            Email = email;
        }

        public Pessoa(string nome, DateTime dataNascimento, string avatar, string senha, string username, string email)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Avatar = avatar;
            Senha = senha;
            Username = username;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Avatar { get; set; }
        public string Senha { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public Pessoa DefinirId(int id)
        {
            Id = id;
            return this;
        }

        public Pessoa AtualizarAvatar(string avatar)
        {
            Avatar = avatar;
            return this;
        }

        public Pessoa AtualizarSenha(string senhaCriptografada)
        {
            Senha = senhaCriptografada;
            return this;
        }

        public Pessoa AtualizarEmail(string email)
        {
            Email = email;
            return this;
        }
    }
}
