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
                        Email = pessoaFisica.Email,
                        Tipo = pessoaFisica.RecuperarTipoCliente(),
                        Usuario = pessoaFisica.Credenciais.Usuario,
                        Senha = pessoaFisica.Credenciais.Senha
                    });
                    if (linhasAfetadas <= 0)
                        return Falha.Nova("Falha ao salvar o seu cadastro. Por favor, tente novamente mais tarde");
                    return pessoaFisica;
                }
                catch (Exception)
                {
                    return Falha.Nova("Falha ao salvar o seu cadastro. Por favor, tente novamente mais tarde");
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }

        }
    }
}
