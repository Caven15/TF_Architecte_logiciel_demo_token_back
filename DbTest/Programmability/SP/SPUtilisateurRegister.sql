CREATE PROCEDURE [dbo].[SPUtilisateurRegister]
	@Nom NVARCHAR(80),
	@Prenom NVARCHAR(80),
	@Email NVARCHAR(100),
	@DateNAissance DATE,
	@Password VARCHAR(50)
AS

BEGIN
	
	DECLARE @PasswordHash BINARY(64), @SecurityStamp UNIQUEIDENTIFIER

	SET @SecurityStamp = NEWID()
	SET @PasswordHash = dbo.FHasher(TRIM(@Password), @SecurityStamp)

	INSERT INTO [Utilisateur] (Nom, Prenom, Email, DateNaissance, PasswordHash, SecurityStamp)
	VALUES (TRIM(@Nom), TRIM(@Prenom), TRIM(@Email), @DateNaissance, @PasswordHash, @SecurityStamp)
END