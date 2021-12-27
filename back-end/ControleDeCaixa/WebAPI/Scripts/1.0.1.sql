CREATE TABLE ControleDeMetas(
	Id INT NOT NULL IDENTITY,
	ValorDesejado DECIMAL(10, 2) not null,
	Descricao VARCHAR(500),
	PessoaId INT NOT NULL
)

ALTER TABLE ControleDeMetas
ADD CONSTRAINT FK_Controle_De_Metas_PessoaId FOREIGN KEY (PessoaId)
    REFERENCES Pessoa(id);