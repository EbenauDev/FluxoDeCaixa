﻿DROP TABLE CaixaCustos;
GO
DROP TABLE CaixaReceitas;
GO
DROP TABLE CaixaHisto;
GO
CREATE TABLE OperacaoCaixa(
	Id INT PRIMARY KEY IDENTITY(1,1),
	TipoOperacao CHAR(1) NOT NULL,
	Valor DECIMAL(10,2) NOT NULL,
	Descricao NVARCHAR(300),
	CaixaMesId INT NOT NULL
)

ALTER TABLE OperacaoCaixa
ADD CONSTRAINT FK_OperacaoCaixa_CaixaMesId FOREIGN KEY (CaixaMesId)
    REFERENCES CaixaMes(id);
GO

CREATE TABLE HistoricoCaixa (
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	TotalReceitas DECIMAL(10, 2) NULL,
	TotalCustos DECIMAL(10, 2) NULL,
	CaixaInicial DECIMAL(10, 2) NULL,
	FluxoDeCaixa DECIMAL(10, 2) NULL,
	CaixaFinal DECIMAL(10, 2) NULL,
	Data DATETIME NOT NULL,
	CaixaMesId INT NOT NULL
);
GO
ALTER TABLE HistoricoCaixa
ADD CONSTRAINT FK_CaixaMes_CaixaMesId FOREIGN KEY (CaixaMesId)
    REFERENCES CaixaMes(id);