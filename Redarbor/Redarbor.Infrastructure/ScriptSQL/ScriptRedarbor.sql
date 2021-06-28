CREATE DATABASE Redarbor;

use Redarbor;

GO

CREATE TABLE [dbo].[Employee] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CompanyId] INT           NOT NULL,
    [CreatedOn] DATETIME      NOT NULL,
    [DeletedOn] DATETIME      NULL,
    [Email]     VARCHAR (254) NULL,
    [Fax]       VARCHAR (50)  NULL,
    [Name]      VARCHAR (50)  NOT NULL,
    [Lastlogin] DATETIME      NULL,
    [Password]  VARCHAR (50)  NULL,
    [PortalId]  INT           NOT NULL,
    [RoleId]    INT           NOT NULL,
    [StatusId]  INT           NOT NULL,
    [Telephone] VARCHAR (50)  NULL,
    [UpdatedOn] DATETIME      NULL,
    [Username]  VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE PROCEDURE [dbo].[ADD_EMPLOYEE]
	@Id int,
	@CompanyId int,
	@CreatedOn datetime,
	@DeletedOn datetime,
	@Email varchar(50),
	@Fax varchar(50),
	@Name varchar(50),
	@Lastlogin datetime, 
	@Password varchar(50),
	@PortalId int,
	@RoleId int,
	@StatusId int,
	@Telephone varchar(50),
	@UpdatedOn varchar(50),
	@Username varchar(50),
	@affectedRows int output
AS
	
	BEGIN TRY
		INSERT INTO [dbo].Employee(
			[CompanyId], 
			[CreatedOn],
			[DeletedOn], 
			[Email], 
			[Fax], 
			[Name], 
			[Lastlogin], 
			[Password], 
			[PortalId], 
			[RoleId], 
			[StatusId], 
			[Telephone], 
			[UpdatedOn], 
			[Username]
		)
		VALUES (
			@CompanyId,
			@CreatedOn,
			@DeletedOn,
			@Email,
			@Fax,
			@Name,
			@Lastlogin, 
			@Password,
			@PortalId,
			@RoleId,
			@StatusId,
			@Telephone,
			@UpdatedOn,
			@Username
		);

		SET @affectedRows = SCOPE_IDENTITY();
  END TRY
  BEGIN CATCH
    SELECT ERROR_MESSAGE() AS ErrorMessage;
  END CATCH


CREATE PROCEDURE [dbo].GET_EMPLOYEE	
@Id int
AS
	SELECT TOP(1) Id,
			CompanyId,
			CreatedOn,
			DeletedOn,
			Email,
			Fax,
			[Name],
			Lastlogin,
			[Password],
			PortalId,
			RoleId,
			StatusId,
			Telephone,
			UpdatedOn,
			Username
	FROM [dbo].Employee
	WHERE Id = @Id;

CREATE PROCEDURE [dbo].GET_EMPLOYEES	
AS
	SELECT	Id,
			CompanyId,
			CreatedOn,
			DeletedOn,
			Email,
			Fax,
			[Name],
			Lastlogin,
			[Password],
			PortalId,
			RoleId,
			StatusId,
			Telephone,
			UpdatedOn,
			Username
	FROM [dbo].Employee;

CREATE PROCEDURE [dbo].[UPDATE_EMPLOYEE]
	@Id int,
	@CompanyId int,
	@CreatedOn datetime,
	@DeletedOn datetime,
	@Email varchar(50),
	@Fax varchar(50),
	@Name varchar(50),
	@Lastlogin datetime, 
	@Password varchar(50),
	@PortalId int,
	@RoleId int,
	@StatusId int,
	@Telephone varchar(50),
	@UpdatedOn varchar(50),
	@Username varchar(50),
	@affectedRows int output
AS
	
	BEGIN TRY
		UPDATE TOP(1) [dbo].Employee
		SET CompanyId = @CompanyId,
			CreatedOn = @CreatedOn,
			DeletedOn = @DeletedOn,
			Email= @Email,
			Fax = @Fax,
			[Name] = @Name,
			Lastlogin = @Lastlogin, 
			[Password] = @Password,
			PortalId = @PortalId,
			RoleId = @RoleId,
			StatusId = @StatusId,
			Telephone = @Telephone,
			UpdatedOn = @UpdatedOn,
			Username = @Username
			WHERE Id = @Id;

		SET @affectedRows = @@ROWCOUNT;
  END TRY
  BEGIN CATCH
    SELECT ERROR_MESSAGE() AS ErrorMessage;
  END CATCH


CREATE PROCEDURE [dbo].[DELETE_EMPLOYEE]
	@Id int,
	@affectedRows int output
AS
	
	BEGIN TRY
		DELETE TOP(1) [dbo].Employee
		WHERE Id = @Id;

		SET @affectedRows = @@ROWCOUNT;
  END TRY
  BEGIN CATCH
    SELECT ERROR_MESSAGE() AS ErrorMessage;
  END CATCH
