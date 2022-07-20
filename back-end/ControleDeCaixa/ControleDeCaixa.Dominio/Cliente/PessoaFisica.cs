using ControleDeCaixa.Core;
using ControleDeCaixa.Core.Compartilhado;
using System;

namespace ControleDeCaixa.Dominio
{

    //Classe de mercação
    public abstract class Cliente : Entity, IAgreggateRoot
    {
        public virtual char RecuperarTipoCliente()
        {
            throw new NotImplementedException();
        }
    }

    public class PessoaFisica : Cliente
    {
        public PessoaFisica(Guid id, string nome, string email, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            TipoCliente = ETipoCliente.Fisica;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string FotoUrl { get; private set; }
        public ETipoCliente TipoCliente { get; private set; }
        public Credenciais Credenciais { get; private set; }

        public static PessoaFisica Nova(string nome, string email, DateTime dataNascimento)
            => new PessoaFisica(Guid.NewGuid(), nome, email, dataNascimento);

        public PessoaFisica AtualizarFoto(string fotoUrl)
        {
            FotoUrl = fotoUrl;
            return this;
        }

        public PessoaFisica AtualizarCredenciais(Credenciais credenciais)
        {
            Credenciais = credenciais;
            return this;
        }

        public override char RecuperarTipoCliente()
            => (char)ETipoCliente.Fisica;
    }
}
