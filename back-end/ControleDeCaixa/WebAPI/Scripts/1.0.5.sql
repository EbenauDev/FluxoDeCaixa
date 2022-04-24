BEGIN TRAN CriarTebelaTransacao;
CREATE TABLE OperacaoTransacao (Id INT NOT NULL PRIMARY KEY IDENTITY,
	Nome VARCHAR(45) NOT NULL,
	Descricao VARCHAR(500),
	Tag VARCHAR(50)
)
COMMIT TRAN CriarTebelaTransacao;

BEGIN TRAN InsercoesTabelaOperacaoTransacao;
INSERT INTO OperacaoTransacao VALUES('Entrada', '', '#Entrada')
GO
INSERT INTO OperacaoTransacao VALUES('Entrada Não Operacional', '', '#EntradaNaoOperacional')
GO
INSERT INTO OperacaoTransacao VALUES('Investimento', '', '#Investimento')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Fixo', '', '#CustoFixo')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Variável', '', '#CustoVariavel')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Não Operacional', '', '#CustoNaoOperacional')
GO
COMMIT TRAN InsercoesTabelaOperacaoTransacao;

BEGIN TRAN AtualizarTabelaOperacaoMes;
ALTER TABLE OperacoesDoMes ADD OperacaoTransacaoId INT REFERENCES OperacaoTransacao(Id)
GO
UPDATE OperacoesDoMes SET OperacaoTransacaoId = 1 WHERE TipoOperacao = 'E';
GO
UPDATE OperacoesDoMes SET OperacaoTransacaoId = 4 WHERE TipoOperacao = 'S';
GO
COMMIT TRAN AtualizarTabelaOperacaoMes;
