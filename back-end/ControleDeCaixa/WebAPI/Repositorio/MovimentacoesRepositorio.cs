using ControleDeCaixa.WebAPI.Entidades;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Models;
using ControleDeCaixa.WebAPI.Repositorio.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public interface IMovimentacoesRepositorio
    {
        Task<Resultado<bool, Falha>> AnoDeMovimentacoesJahExiste(int ano, int pessoaId);
        Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacaoAsync(MovimentacaoAnual movimentacaoAnual);
        Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacoesAsync(MovimentacaoMes mesDeMovimentacoes);
        Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacaoMes mesDeMovimentacoes);
        Task<Resultado<IEnumerable<MesDeMovimentacaoDTO>, Falha>> RecuperarMesesDeMovimentacaoDoAnoAsync(int pessoaId, int anoId);
        Task<Resultado<IEnumerable<AnoDeMovimentacaoDTO>, Falha>> RecuperarAnosDeMovimentacoesAsync(int pessoaId);
    }

    public class MovimentacoesRepositorio : IMovimentacoesRepositorio
    {
        private readonly string _connectionString;
        public MovimentacoesRepositorio(IConnectionHelper connectionHelper)
        {
            _connectionString = connectionHelper.GetConnectionString();
        }

        public async Task<Resultado<bool, Falha>> AnoDeMovimentacoesJahExiste(int ano, int pessoaId)
        {
            const string sql = @"SELECT IIF(COUNT(Id) > 0, 1, 0) AS AnoDeMovimentacoesExiste
                                 FROM MovimentacoesAnuais(NOLOCK)
                                 WHERE Ano = @ano AND IdPessoa = @pessoaId;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return await conexao.QueryFirstOrDefaultAsync<bool>(sql, new { ano, pessoaId });
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao verificar se o registro para o ano {ano} já existe", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<MovimentacaoAnual, Falha>> NovoAnoDeMovimentacaoAsync(MovimentacaoAnual movimentacaoAnual)
        {
            const string sql = @"INSERT INTO MovimentacoesAnuais(IdPessoa, Ano, Descricao, DataDeCriacao)
                                 VALUES(@IdPessoa, @Ano, @Descricao, @DataDeCriacao);

                                 SELECT SCOPE_IDENTITY() AS Id;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultadoIdMovimentacaoAnual = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        IdPessoa = movimentacaoAnual.PessoaId,
                        movimentacaoAnual.Ano,
                        movimentacaoAnual.Descricao,
                        movimentacaoAnual.DataDeCriacao
                    });
                    if (resultadoIdMovimentacaoAnual == 0)
                        return Falha.Nova("Houve um problema ao tentar salvar o novo ano de movimentações");
                    movimentacaoAnual.DefinirId(resultadoIdMovimentacaoAnual);
                    return movimentacaoAnual;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao salvar o ano de movimentações", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<MovimentacaoMes, Falha>> NovoMesDeMovimentacoesAsync(MovimentacaoMes mesDeMovimentacoes)
        {
            const string sql = @"INSERT INTO MesDeMovimentacoes(IdAnoMovimentacoes, MesDeReferencia, Descricao, DataDeCriacao)
                                 VALUES(@IdAnoMovimentacoes, @MesDeReferencia, @Descricao, @DataDeCriacao);

                                 SELECT SCOPE_IDENTITY() AS Id;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultadoIdMovimentacaoAnual = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        mesDeMovimentacoes.IdAnoMovimentacoes,
                        mesDeMovimentacoes.MesDeReferencia,
                        mesDeMovimentacoes.Descricao,
                        DataDeCriacao = DateTime.Now
                    });
                    if (resultadoIdMovimentacaoAnual == 0)
                        return Falha.Nova("Houve um problema ao tentar salvar o novo ano de movimentações");
                    mesDeMovimentacoes.DefinirId(resultadoIdMovimentacaoAnual);
                    return mesDeMovimentacoes;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao salvar o ano de movimentações", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<OperacaoMes, Falha>> NovaOperacaoNoMesAsync(OperacaoMes operacaoMes)
        {
            const string sql = @"INSERT INTO OperacoesDoMes(Valor, MesId, Descricao, TipoOperacao, DataDeRegistro)
                                 VALUES(@Valor, @MesId, @Descricao, @TipoOperacao, @DataDeRegistro);

                                 SELECT SCOPE_IDENTITY() AS Id;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultadoIdMovimentacaoAnual = await conexao.QueryFirstOrDefaultAsync<int>(sql, new
                    {
                        operacaoMes.Valor,
                        operacaoMes.MesId,
                        operacaoMes.Descricao,
                        TipoOperacao = (char)operacaoMes.TipoOperacao,
                        DataDeRegistro = DateTime.Now
                    });
                    if (resultadoIdMovimentacaoAnual == 0)
                        return Falha.Nova("Houve um problema ao tentar salvar o novo ano de movimentações");
                    operacaoMes.DefinirId(resultadoIdMovimentacaoAnual);
                    return operacaoMes;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException("Falha ao salvar o ano de movimentações", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }

        }

        public async Task<Resultado<IEnumerable<MesDeMovimentacaoDTO>, Falha>> RecuperarMesesDeMovimentacaoDoAnoAsync(int pessoaId, int anoId)
        {
            const string sql = @"SELECT MesDeMovimentacoes.Id,
	                                    MesDeMovimentacoes.MesDeReferencia,
	                                    MesDeMovimentacoes.DataDeCriacao
	                                    FROM MovimentacoesAnuais (NOLOCK) 
                                 INNER JOIN MesDeMovimentacoes (NOLOCK) 
                                 ON MesDeMovimentacoes.IdAnoMovimentacoes =  MovimentacoesAnuais.Id
                                 WHERE MovimentacoesAnuais.IdPessoa = @pessoaId AND MovimentacoesAnuais.Id = @anoId;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return (await conexao.QueryAsync<MesDeMovimentacaoDTO>(sql, new { pessoaId, anoId })).ToList();
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao recuperar meses de movimentações para o ano {anoId}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<IEnumerable<AnoDeMovimentacaoDTO>, Falha>> RecuperarAnosDeMovimentacoesAsync(int pessoaId)
        {
            const string sql = @"SELECT Id,
	                                    Ano,
	                                    Descricao,
	                                    DataDeCriacao
                                 FROM MovimentacoesAnuais (NOLOCK)
                                 WHERE IdPessoa = @pessoaId;";
            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return (await conexao.QueryAsync<AnoDeMovimentacaoDTO>(sql, new { pessoaId })).ToList();
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao recuperar os anos de movimentações para a pessoa {pessoaId}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
