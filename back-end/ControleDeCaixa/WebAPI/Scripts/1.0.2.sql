CREATE PROCEDURE ResumoMetas
(@PessoaId INT)
AS 
BEGIN
SET NOCOUNT ON

DECLARE @SaldoEntradas DECIMAL(10, 2) = 0;
DECLARE @SaldoSaidas DECIMAL(10, 2) = 0;


SELECT @SaldoEntradas = SUM(IIF(Valor IS NULL, 0, Valor)) 
FROM OperacoesDoMes
INNER JOIN MesDeMovimentacoes ON MesDeMovimentacoes.Id = OperacoesDoMes.MesId
INNER JOIN MovimentacoesAnuais ON MovimentacoesAnuais.Id = MesDeMovimentacoes.IdAnoMovimentacoes
WHERE OperacoesDoMes.TipoOperacao = 'E' AND MovimentacoesAnuais.IdPessoa = @PessoaId;

SELECT @SaldoSaidas = SUM(IIF(Valor IS NULL, 0, Valor)) 
FROM OperacoesDoMes
INNER JOIN MesDeMovimentacoes ON MesDeMovimentacoes.Id = OperacoesDoMes.MesId
INNER JOIN MovimentacoesAnuais ON MovimentacoesAnuais.Id = MesDeMovimentacoes.IdAnoMovimentacoes
WHERE OperacoesDoMes.TipoOperacao = 'S' AND MovimentacoesAnuais.IdPessoa = @PessoaId;


SELECT Id, 
	   ValorDesejado,
	   Descricao,
	   (@SaldoEntradas - @SaldoSaidas) AS Saldo,
	   IIF((ValorDesejado - (@SaldoEntradas - @SaldoSaidas)) >= ValorDesejado, 0, (ValorDesejado - (@SaldoEntradas - @SaldoSaidas))) AS ValorRestante,
	   CAST(IIF(((@SaldoEntradas - @SaldoSaidas) / ValorDesejado) > 1, 100, ((@SaldoEntradas - @SaldoSaidas) / ValorDesejado) * 100) AS numeric(10,2)) AS ProgressoMeta
	   FROM ControleDeMetas
	   WHERE PessoaId = @PessoaId;
END
