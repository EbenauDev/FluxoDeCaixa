namespace ControleDeCaixa.WebAPI.Entidades
{
    public class PessoaResumida
    {
        public PessoaResumida(int id, string email, string avatar)
        {
            Id = id;
            Email = email;
            Avatar = avatar;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
