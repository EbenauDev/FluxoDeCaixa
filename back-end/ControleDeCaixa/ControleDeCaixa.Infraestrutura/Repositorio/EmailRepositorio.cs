using ControleDeCaixa.Compartilhado.Generics;
using ControleDeCaixa.Dominio.Views;
using ControleDeCaixa.Infraestrutura.Helper;
using ControleDeCaixa.Infraestrutura.Repositorio.DTO;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.Infraestrutura.Repositorio
{

    public interface IEmailRepositorio
    {
        Task<Resultado<TemplateEmailViewModel, Falha>> RecuperarTemplateEmailAsync(string tipoUso);
    }

    public class EmailRepositorio : IEmailRepositorio
    {
        private readonly string _connectionString;
        public EmailRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<TemplateEmailViewModel, Falha>> RecuperarTemplateEmailAsync(string tipoUso)
        {
            const string sql = @"SELECT Id, 
                                        HTML As Html,
	                                    Assunto,
	                                    EnderecoDeOrigem
                                 FROM TemplatesEmail
                                 WHERE TipoDeUso = @TipoDeUso;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.QueryFirstOrDefaultAsync<TemplateEmailDTO>(sql, new { TipoDeUso = tipoUso });
                    if (resultado is null)
                        return Falha.Nova($"Nenhum template de e-mail foi encontrado para o tipo de uso {tipoUso}");
                    return new TemplateEmailViewModel(resultado.Id,
                                                      resultado.TipoDeUso,
                                                      resultado.HTML,
                                                      resultado.Assunto,
                                                      resultado.EnderecoOrigem);
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao recuperar template de e-mail para o tipo de uso {tipoUso}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
