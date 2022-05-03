CREATE PROCEDURE MovimentacoesMes @MesId INT
AS
(
	SELECT OperacoesDoMes.Id,
		   OperacoesDoMes.Valor,
		   OperacoesDoMes.Descricao,
		   OperacaoTransacao.Nome AS NomeTransacao,
		   OperacaoTransacao.Tag AS TagTransacao
	FROM OperacoesDoMes
	INNER JOIN OperacaoTransacao
		ON OperacaoTransacao.Id = OperacoesDoMes.OperacaoTransacaoId
	WHERE OperacoesDoMes.MesId = @MesId
	GROUP BY OperacoesDoMes.Id,
			 OperacoesDoMes.Valor,
			 OperacoesDoMes.Descricao,
			 OperacaoTransacao.Nome,
			 OperacaoTransacao.Tag
)
