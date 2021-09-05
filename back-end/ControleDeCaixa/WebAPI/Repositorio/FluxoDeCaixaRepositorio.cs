using ControleDeCaixa.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeCaixa.WebAPI.Repositorio
{
    public class FluxoDeCaixaRepositorio : IFluxoDeCaixaRepositorio
    {
        private readonly string _stringConexao;

        public async Task<IEnumerable<FluxoDeCaixaAnual>> RecuperarFluxoDeCaixa(string ano)
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

            using var conexao = new SqlConnection(_stringConexao);
            try
            {
                await conexao.OpenAsync();
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
