BEGIN TRAN CriarTebelaTransacao;
CREATE TABLE OperacaoTransacao (Id INT NOT NULL PRIMARY KEY IDENTITY,
	Nome VARCHAR(45) NOT NULL,
	Descricao VARCHAR(500),
	Tag VARCHAR(50),
	Tipo CHAR(1)
)
COMMIT TRAN CriarTebelaTransacao;

BEGIN TRAN InsercoesTabelaOperacaoTransacao;
INSERT INTO OperacaoTransacao VALUES('Entrada', '', '#Entrada', 'E')
GO
INSERT INTO OperacaoTransacao VALUES('Entrada Não Operacional', '', '#EntradaNaoOperacional', 'E')
GO
INSERT INTO OperacaoTransacao VALUES('Investimento', '', '#Investimento', 'E')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Fixo', '', '#CustoFixo', 'C')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Variável', '', '#CustoVariavel', 'C')
GO
INSERT INTO OperacaoTransacao VALUES('Custo Não Operacional', '', '#CustoNaoOperacional', 'C')
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

BEGIN TRAN CriarColunaMes
ALTER TABLE MesDeMovimentacoes ADD Mes VARCHAR(15);
GO
ALTER TABLE MesDeMovimentacoes DROP COLUMN MesDeReferencia;
GO
COMMIT TRAN CriarColunaMes