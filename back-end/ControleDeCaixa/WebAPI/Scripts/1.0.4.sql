IF NOT EXISTS (SELECT JahUtilizado, DataUtilizacao  FROM TokenSenha)
BEGIN
	ALTER TABLE TokenSenha 
	ADD JahUtilizado BIT NULL DEFAULT 0,
	DataUtilizacao DATETIME NULL;
END

