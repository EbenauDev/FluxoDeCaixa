﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ControleDeCaixa.WebAPI.Generics;
using ControleDeCaixa.WebAPI.Helper;
using ControleDeCaixa.WebAPI.Models;
using Dapper;

namespace ControleDeCaixa.WebAPI.Repositorio
{

    public interface IMetasRepositorio
    {
        Task<Resultado<bool, Falha>> AdicionarNovaMetaAsync(NovaMeta meta, int pessoaId);
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
    }
}
