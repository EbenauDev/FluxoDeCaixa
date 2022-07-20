using ControleDeCaixa.Core.Compartilhado;
using ControleDeCaixa.Core.Utils;
using ControleDeCaixa.Dominio;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infra.Cliente
{
    public sealed class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _stringConexao;
        public ClienteRepositorio(IConnectionStringHelper _helper)
        {
            _stringConexao = _helper.GetConnection();
        }

        public async Task<Resultado<PessoaFisica, Falha>> SalvarRegistroClienteAsync(PessoaFisica pessoaFisica)
        {
            const string sql = @"INSERT INTO Pessoa(Id, Nome, Email, Tipo, Usuario, Senha)
                                 VALUES(@Id, @Nome, @Email, @Tipo, @Usuario, @Senha);";


            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        Id = pessoaFisica.Id,
                        Nome = pessoaFisica.Nome,
                        Email = pessoaFisica.ema,
                        Tipo,
                        Usuario,
                        Senha
                    });
                }
                catch (Exception)
                {

                }
            }
            return Falha.Nova("Método de salvar cliente não foi implementado");
        }
    }
}
