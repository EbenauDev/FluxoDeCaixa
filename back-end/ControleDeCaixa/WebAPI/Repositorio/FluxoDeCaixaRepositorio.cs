using ControleDeCaixa.WebAPI.DTO;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Models;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class FluxoDeCaixaRepositorio : IFluxoDeCaixaRepositorio
    {

        public async Task<FluxoDeCaixaAnual> RecuperarFluxoDeCaixa(string ano)
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
    }
}
