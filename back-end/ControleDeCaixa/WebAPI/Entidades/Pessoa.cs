namespace ControleDeCaixa.WebAPI.Entidades
{
    public class Pessoa
    {
        public Pessoa(int id, string avatar, string senha, string username, string email)
        {
            Id = id;
            Avatar = avatar;
            Senha = senha;
            Username = username;
            Email = email;
        }

        public Pessoa(string avatar, string senha, string username, string email)
        {
            Avatar = avatar;
            Senha = senha;
            Username = username;
            Email = email;
        }

        public int Id { get; set; }
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
