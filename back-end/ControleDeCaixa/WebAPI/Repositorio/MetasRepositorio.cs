using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio.DTO;
using ControleDeCaixa.WebAPI.Views;
using Dapper;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IMetasRepositorio
    {
        Task<Resultado<bool, Falha>> AdicionarNovaMetaAsync(NovaMeta meta, int pessoaId);
        Task<Resultado<bool, Falha>> AtualizarMetaAsync(MetaAtualizada meta, int pessoaId);
        Task<Resultado<IEnumerable<ResumoMetasViewModel>, Falha>> RecuperarResumoMetas(int pessoaId);
    }

    public class MetasRepositorio : IMetasRepositorio
    {
        private readonly string _stringDeConexao;
        public MetasRepositorio(IConnectionHelper connection)
        {
            _stringDeConexao = connection.GetConnectionString();
        }

        public async Task<Resultado<bool, Falha>> AdicionarNovaMetaAsync(NovaMeta meta, int pessoaId)
        {
            const string sql = @"INSERT INTO ControleDeMetas(
	                                    ValorDesejado,
	                                    Descricao,
	                                    PessoaId
                                    )
                                    VALUES(@ValorDesejado, 
	                                       @Descricao, 
	                                       @PessoaId);";
            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new { meta.ValorDesejado, meta.Descricao, PessoaId = pessoaId });
                   if (linhasAfetadas == 0)
                        return Falha.Nova("Houve um problema ao definir a nova meta no sistema");
                    return true;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao definir a nova meta no sistema", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<bool, Falha>> AtualizarMetaAsync(MetaAtualizada meta, int pessoaId)
        {
            const string sql = @"UPDATE ControleDeMetas SET  ValorDesejado = @Valor,
	                                                         Descricao = @Descricao
                                 WHERE Id = @Id AND PessoaId = @PessoaId";
            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        meta.Id,
                        meta.Valor,
                        meta.Descricao,
                        PessoaId = pessoaId
                    });
                    if (linhasAfetadas == 0)
                        return Falha.Nova("Houve um problema ao atualizar meta no sistema");
                    return true;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao atualizar meta no sistema", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<IEnumerable<ResumoMetasViewModel>, Falha>> RecuperarResumoMetas(int pessoaId)
        {
            const string sql = @"EXEC ResumoMetas @PessoaId";
            using (var conexao = new SqlConnection(_stringDeConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    return (await conexao.QueryAsync<ResumoMetasDTO>(sql, new { PessoaId = pessoaId })).ConverterParaViewModel().ToList();
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao recuperar o resumo das metas", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
