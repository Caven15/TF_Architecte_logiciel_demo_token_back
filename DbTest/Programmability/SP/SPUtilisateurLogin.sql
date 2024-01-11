CREATE PROCEDURE [dbo].[SPUtilisateurLogin]
	@Email NVARCHAR(100),
	@Password VARCHAR(50)
AS

BEGIN

	DECLARE @PasswordHash BINARY(64), @SecurityStamp UNIQUEIDENTIFIER

	SET @SecurityStamp = (SELECT SecurityStamp FROM [Utilisateur] WHERE Email = @Email)
	SET @PasswordHash = dbo.FHasher(@Password, @SecurityStamp)


	IF EXISTS (Select TOP 1 * FROM [Utilisateur] WHERE Email = @Email AND PasswordHash = @PasswordHash)
	BEGIN

		SELECT * INTO #TempUser
		FROM [Utilisateur]
		WHERE Email = @Email
		ALTER TABLE #TempUser
		DROP COLUMN PasswordHash, SecurityStamp
		SELECT * FROM #TempUser
		DROP TABLE #TempUser
	END

	RETURN 0
END