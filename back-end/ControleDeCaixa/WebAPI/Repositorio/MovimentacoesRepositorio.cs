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
        Task<Resultado<IEnumerable<MesDeMovimentacaoDTO>, Falha>> RecuperarMesesDeMovimentacaoDoAnoAsync(int pessoaId, int anoId);
        Task<Resultado<IEnumerable<AnoDeMovimentacaoDTO>, Falha>> RecuperarAnosDeMovimentacoesAsync(int pessoaId);
        Task<Resultado<MovimentacaoMes, Falha>> ListarMovimentacoesDoMesAsync(int anoId, int mesId);
        Task<Resultado<bool, Falha>> ExcluirOperacaoDoMesAsync(int mesId, int operacaoMesId);
        Task<Resultado<HistoricoAnual, Falha>> HistoricoMovimentacoesAsync(int pessoaId, int anoId);
        Task<Resultado<bool, Falha>> MesDeMovimentacaoJahExisteAsync(int anoId, int mes);

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

        public async Task<Resultado<MovimentacaoMes, Falha>> ListarMovimentacoesDoMesAsync(int anoId, int mesId)
        {
            const string sql = @"SELECT Id,
                                        IdAnoMovimentacoes,
	                                    MesDeReferencia, 
	                                    Descricao
                                 FROM MesDeMovimentacoes (NOLOCK)
                                 WHERE IdAnoMovimentacoes = @AnoId AND Id = @MesId;

                                 SELECT OperacoesDoMes.Id, 
	                                    Valor, 
	                                    MesId, 
	                                    OperacoesDoMes.Descricao, 
	                                    TipoOperacao
                                 FROM OperacoesDoMes (NOLOCK)
                                 INNER JOIN MesDeMovimentacoes ON MesDeMovimentacoes.Id = OperacoesDoMes.MesId 
                                            AND MesDeMovimentacoes.IdAnoMovimentacoes = @AnoId AND OperacoesDoMes.MesId = @MesId";

            using (var conexao = new SqlConnection(_connectionString))
            {
                await conexao.OpenAsync();
                using (var queryMultipla = await conexao.QueryMultipleAsync(sql, new { AnoId = anoId, MesId = mesId }))
                {
                    try
                    {
                        var mesDeMovimentacao = (await queryMultipla.ReadAsync<dynamic>())
                                       .Select(m => new MovimentacaoMes((int)m.Id, (int)m.IdAnoMovimentacoes, m.MesDeReferencia, m.Descricao))
                                       .FirstOrDefault();

                        var movimentacoesMes = (await queryMultipla.ReadAsync<dynamic>());
                        if (movimentacoesMes.Any())
                        {
                            var operacoes = movimentacoesMes.Select(o => new OperacaoMes((int)o.Id,
                                                   (int)o.MesId,
                                                   (int)o.OperacaoTransacaoId,
                                                   (decimal)o.Valor,
                                                   (string)o.Descricao));

                            mesDeMovimentacao.DefinirDespesas(operacoes.Where(o => o.OperacaoTransacaoId == 4));
                            mesDeMovimentacao.DefinirReceitas(operacoes.Where(o => o.OperacaoTransacaoId == 1));
                            mesDeMovimentacao.DefinirSaldo(new Saldo(
                                totalReceitas: operacoes.Where(o => o.OperacaoTransacaoId == 1).Sum(o => o.Valor),
                                totalDespesas: operacoes.Where(o => o.OperacaoTransacaoId == 4).Sum(o => o.Valor)
                                ));
                        }
                        return mesDeMovimentacao;
                    }
                    catch (Exception ex)
                    {
                        return Falha.NovaComException($"Falha ao carregar as movimentações para o mês {mesId}", ex);
                    }
                    finally
                    {
                        await conexao.CloseAsync();
                    }
                }
            }
        }

        public async Task<Resultado<bool, Falha>> ExcluirOperacaoDoMesAsync(int mesId, int operacaoMesId)
        {
            const string sql = @"DELETE FROM OperacoesDoMes WHERE Id = @operacaoMesId AND MesId = @mesId;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        mesId,
                        operacaoMesId
                    });
                    if (linhasAfetadas == 0)
                        return Falha.Nova($"Houve um problema ao tentar remover as operações do mês {mesId}");
                    return true;
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao tentar remover as operações do mês {mesId}", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Resultado<HistoricoAnual, Falha>> HistoricoMovimentacoesAsync(int pessoaId, int anoId)
        {
            const string sql = @"SELECT Id, 
	                                    Ano, 
	                                    Descricao, 
	                                    DataDeCriacao  
                                 FROM MovimentacoesAnuais (NOLOCK)
                                 WHERE Id = @AnoId AND IdPessoa = @PessoaId;

                                 SELECT Id,
                                        IdAnoMovimentacoes,
	                                    MesDeReferencia, 
	                                    Descricao
                                 FROM MesDeMovimentacoes (NOLOCK)
                                 WHERE IdAnoMovimentacoes = @AnoId;

                                 SELECT OperacoesDoMes.Id, 
	                                    Valor, 
	                                    MesId,
                                        OperacaoTransacaoId,
	                                    OperacoesDoMes.Descricao
                                 FROM OperacoesDoMes (NOLOCK)
                                 INNER JOIN MesDeMovimentacoes ON MesDeMovimentacoes.Id = OperacoesDoMes.MesId AND MesDeMovimentacoes.IdAnoMovimentacoes = @AnoId;";

            using (var conexao = new SqlConnection(_connectionString))
            {
                await conexao.OpenAsync();
                try
                {
                    using (var queryMultipla = await conexao.QueryMultipleAsync(sql, new { AnoId = anoId, PessoaId = pessoaId }))
                    {
                        var ano = await queryMultipla.ReadFirstOrDefaultAsync<AnoDeMovimentacaoDTO>();
                        var meses = (await queryMultipla.ReadAsync<dynamic>())
                                        .Select(m => new MovimentacaoMes((int)m.Id, (int)m.IdAnoMovimentacoes, m.MesDeReferencia, m.Descricao));

                        var movimentacoesMes = (await queryMultipla.ReadAsync<dynamic>())
                                        .Select(o => new OperacaoMes((int)o.Id,
                                                (int)o.MesId,
                                                (int)o.MesId,
                                                (decimal)o.Valor,
                                                (string)o.Descricao));

                        return new HistoricoAnual(ano.Id,
                                                  ano.Ano,
                                                  ano.DataDeCriacao,
                                                  movimentacoesMes: meses.Select(m => new HistoricoMes(m.Id,
                                                              m.MesDeReferencia,
                                                              m.Descricao,
                                                              movimentacoesMes.Where(movimentacao => movimentacao.MesId == m.Id)
                                                  )));
                    }
                }
                catch (Exception ex)
                {

                    return Falha.NovaComException($"Falha ao atualizar operação do mês", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }


        public async Task<Resultado<bool, Falha>> MesDeMovimentacaoJahExisteAsync(int anoId, int mes)
        {
            const string sql = @"SELECT COUNT(Id) AS MesJahExiste 
                                  FROM MesDeMovimentacoes (NOLOCK)
                                  WHERE IdAnoMovimentacoes = @AnoId AND (MONTH(DataDeCriacao) = @MesReferencia)";

            using (var conexao = new SqlConnection(_connectionString))
            {
                try
                {
                    await conexao.OpenAsync();
                    return await conexao.QueryFirstOrDefaultAsync<bool>(sql, new
                    {
                        AnoId = anoId,
                        MesReferencia = mes
                    });
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException($"Falha ao verificar se o mês {mes} já existe para o ano informado", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}

