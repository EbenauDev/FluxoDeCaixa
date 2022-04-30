using ControleDeCaixa.Compartilhado.Generics;
using ControleDeCaixa.Dominio.Entidades;
using ControleDeCaixa.Infraestrutura.Helper;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infraestrutura.Repositorio
{

    public interface IPessoaRepositorio
    {
        Task<Resultado<Pessoa, Falha>> NovaPessoaAsync(Pessoa pessoa);
        Task<Resultado<Pessoa, Falha>> AtualizarPessoaAsync(Pessoa pessoa);
        Task<Resultado<Pessoa, Falha>> RecuperarPessoaPorIdAsync(int pessoaId);
        Task<Resultado<Pessoa, Falha>> RecuperarPessoaPorUsername(string username);
        Task<Resultado<bool, Falha>> UsernameEstahDisponivelAsync(string username);
    }

    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly string _connectionString;
        public PessoaRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<Pessoa, Falha>> NovaPessoaAsync(Pessoa pessoa)
        {
            const string sql = @"INSERT INTO Pessoa (Nome, DataNascimento, Avatar, Senha, Username, Email)
                                 VALUES(@Nome, @DataNascimento, @Avatar, @Senha, @Username, @Email);

                                 SELECT SCOPE_IDENTITY() AS Id;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var idConfiguracaoPessoa = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        pessoa.Nome,
                        pessoa.DataNascimento,
                        pessoa.Avatar,
                        pessoa.Senha,
                        pessoa.Username,
                        pessoa.Email
                    });
                    if (idConfiguracaoPessoa == 0)
                        return Falha.Nova($"Houve um problema ao tentar criar a conta para {pessoa.Username}");
                    pessoa.DefinirId(idConfiguracaoPessoa);
                    return pessoa;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao incluir um novo registro para {pessoa.Username}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
        public async Task<Resultado<bool, Falha>> UsernameEstahDisponivelAsync(string username)
        {
            const string sql = @"DECLARE @UsernameParaPesquisar VARCHAR(45) = @username;
            
                                SELECT IIF(COUNT(Username) > 0, 1, 0) AS UsernameJahEstahEmUso
                                FROM Pessoa (NOLOCK)
                                WHERE Username = @UsernameParaPesquisar;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return await conexao.QueryFirstOrDefaultAsync<bool>(sql, new { username });
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao verificar se o username {username} já existe", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<Pessoa, Falha>> RecuperarPessoaPorIdAsync(int pessoaId)
        {
            const string sql = @"SELECT Id, 
                                        Nome, 
                                        DataNascimento, 
                                        Avatar, 
                                        Senha, 
                                        Username, 
                                        Email
                                 FROM Pessoa
                                 WHERE Id = @pessoaId";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var pessoa = await conexao.QueryFirstOrDefaultAsync<Pessoa>(sql, new { pessoaId });
                    if (pessoa is null)
                        return Falha.Nova($"Houve um problema ao recuperar pessoa de Id {pessoaId}");
                    return pessoa;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao recuperar pessoa de Id{pessoaId}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<Pessoa, Falha>> AtualizarPessoaAsync(Pessoa pessoa)
        {
            const string sql = @"UPDATE Pessoa 
                                 SET Email = @Email, 
                                     Senha = @Senha, 
                                     Avatar = @Avatar
                                 WHERE Id = @pessoaId";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.ExecuteAsync(sql, new
                    {
                        pessoa.Email,
                        pessoa.Senha,
                        pessoa.Avatar,
                        pessoaId = pessoa.Id,
                    });
                    if (resultado <= 0)
                        return Falha.Nova($"Houve um problema ao atualizar pessoa de Id {pessoa.Id}");
                    return pessoa;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao atualizar pessoa de Id {pessoa.Id}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<Pessoa, Falha>> RecuperarPessoaPorUsername(string username)
        {
            const string sql = @"SELECT Id, 
                                        Nome, 
                                        DataNascimento, 
                                        Avatar, 
                                        Senha, 
                                        Username, 
                                        Email
                                 FROM Pessoa
                                 WHERE Username = @Username";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var pessoa = await conexao.QueryFirstOrDefaultAsync<Pessoa>(sql, new { Username = username });
                    if (pessoa is null)
                        return Falha.Nova($"Houve um problema ao recuperar pessoa de Id {username}");
                    return pessoa;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao recuperar pessoa de Id{username}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
