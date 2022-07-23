CREATE PROCEDURE UsuarioEstahDisponivel
	@UsuarioPesquisa VARCHAR(30)
AS
	SELECT IIF(COUNT(*) > 0, 1, 0) AS Resultado 
	FROM Pessoa
	WHERE Usuario = @UsuarioPesquisa;
