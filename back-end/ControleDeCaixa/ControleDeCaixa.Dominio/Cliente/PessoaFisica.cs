using ControleDeCaixa.Core;
using ControleDeCaixa.Core.Compartilhado;
using System;

namespace ControleDeCaixa.Dominio
{
    public class PessoaFisica : Entity, IAgreggateRoot
    {
        public PessoaFisica(Guid id, string nome, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            TipoCliente = ETipoCliente.Fisica;
        }

        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string FotoUrl { get; private set; }
        public ETipoCliente TipoCliente { get; private set; }
        public Credenciais Credenciais { get; private set; }

        public static PessoaFisica Nova(string nome, DateTime dataNascimento, Credenciais credenciais)
            => new PessoaFisica(Guid.NewGuid(), nome, dataNascimento)
                            .AtualizarCredenciais(credenciais);

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
    }
}
