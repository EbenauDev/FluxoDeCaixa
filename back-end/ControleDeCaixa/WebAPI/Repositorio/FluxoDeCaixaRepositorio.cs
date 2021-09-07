using ControleDeCaixa.WebAPI.DTO;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Entities;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Models;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class FluxoDeCaixaRepositorio : IFluxoDeCaixaRepositorio
    {

        public async Task<FluxoDeCaixaAnual> RecuperarFluxoDeCaixaAsync(string ano)
        {
            const string sql = @"DECLARE @AnoReferencia VARCHAR(4);
								 SET @AnoReferencia = @ano;
								 
								 SELECT FluxoCaixaAnual.Id AS CaixaAnualId, 
								 	   FluxoCaixaAnual.Ano,
								 	   CaixaMes.Id AS CaixaMesId,
								 	   CaixaMes.MesReferencia,
								 	   CaixaReceitas.Id AS ReceitaId,
								 	   CaixaReceitas.Valor AS ValorReceita,
								 	   CaixaReceitas.Descricao AS  DescricaoReceita,
								 	   CaixaCustos.Id AS CustoId,
								 	   CaixaCustos.Valor AS ValorCusto,
								 	   CaixaCustos.Descricao AS DescricaoCusto
								 FROM FluxoCaixaAnual (NOLOCK)
								 INNER JOIN CaixaMes (NOLOCK) ON CaixaMes.FluxoCaixaAnualId = FluxoCaixaAnual.Id
								 LEFT JOIN CaixaReceitas (NOLOCK) ON CaixaReceitas.CaixaMesId = CaixaMes.Id
								 LEFT JOIN CaixaCustos (NOLOCK) ON CaixaCustos.CaixaMesId = CaixaMes.Id
								 WHERE FluxoCaixaAnual.Ano = @AnoReferencia";

            using var conexao = new SqlConnection(Connection.ConnectionValue());
            try
            {
                await conexao.OpenAsync();
                return (await conexao.QueryAsync<FluxoDeCaixaDTO>(sql, new { ano })).ConverterParaModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }

        public async Task<bool> NovoCustoAsync(CustoInputModel custo)
        {
            const string sql = @"INSERT INTO CaixaCustos (Valor, Descricao, CaixaMesId)
                                 VALUES(@Valor, @Descricao, @CaixaMesId)";
            using var conexao = new SqlConnection(Connection.ConnectionValue());
            try
            {
                await conexao.OpenAsync();
                if (await conexao.ExecuteAsync(sql, custo) is var resultado && resultado <= 0)
                    throw new Exception("Falha ao inserir um novo custo");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }

        public async Task<bool> NovaReceitaAsync(ReceitaInputModel receita)
        {
            const string sql = @"INSERT INTO CaixaReceitas (Valor, Descricao, CaixaMesId)
                                 VALUES(@Valor, @Descricao, @CaixaMesId)";
            using var conexao = new SqlConnection(Connection.ConnectionValue());
            try
            {
                await conexao.OpenAsync();
                if (await conexao.ExecuteAsync(sql, receita) is var resultado && resultado <= 0)
                    throw new Exception("Falha ao inserir um novo custo");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }
}
