using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Repositorio.DTO;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface ISenhasRepositorio
    {
        Task<Resultado<TokenSenha, Falha>> GravarTokenNovaSenhaAsync(TokenSenha token);
        Task<Resultado<TokenSenha, Falha>> RecuperarTokenSenhaAsync(Guid tokenSenha);
    }
    public class SenhasRepositorio : ISenhasRepositorio
    {
        private readonly string _connectionString;
        public SenhasRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<TokenSenha, Falha>> GravarTokenNovaSenhaAsync(TokenSenha token)
        {
            const string sql = @"INSERT INTO TokenSenha(Id, 
					                                   ExpiraEm, 
					                                   GeradoEm, 
					                                   DataDeRegistro, 
					                                   PessoaId, 
					                                   JahUtilizado, 
					                                   DataUtilizacao)
                                                 VALUES(@Id, 
					                                   @ExpiraEm, 
					                                   @GeradoEm, 
					                                   @DataDeRegistro, 
					                                   @PessoaId, 
					                                   @JahUtilizado, 
					                                   @DataUtilizacao);";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.ExecuteAsync(sql, new
                    {
                        token.Id,
                        token.ExpiraEm,
                        token.GeradoEm,
                        token.DataDeRegistro,
                        token.PessoaId,
                        token.JahUtilizado,
                        token.DataUtilizacao
                    });
                    if (resultado < 0)
                        return Falha.Nova("Falha ao gravar o token de senha.");
                    return token;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao gravar o token de senha", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<TokenSenha, Falha>> RecuperarTokenSenhaAsync(Guid tokenSenha)
        {
            const string sql = @"SELECT Id, 
	                                    ExpiraEm, 
	                                    GeradoEm, 
	                                    DataDeRegistro, 
	                                    PessoaId, 
	                                    JahUtilizado, 
	                                    DataUtilizacao
                                 FROM TokenSenha (NOLOCK)
                                 WHERE Id = @Id;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<TokenSenhaDTO>(sql, new { Id = tokenSenha });
                    if (resultado is null)
                        return Falha.Nova($"Não foi encontrado o token de senha para o Id {tokenSenha}");
                    return new TokenSenha(resultado.Id,
                        resultado.ExpiraEm,
                        resultado.GeradoEm,
                        resultado.DataDeRegistro,
                        resultado.PessoaId);
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao gravar o token de senha", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
