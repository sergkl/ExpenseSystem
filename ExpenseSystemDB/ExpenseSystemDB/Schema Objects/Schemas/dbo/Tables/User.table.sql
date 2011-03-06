CREATE TABLE [dbo].[User] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50) NOT NULL,
    [MiddleName] NVARCHAR (50) NULL,
    [LastName]   NVARCHAR (50) NOT NULL,
    [Email]      NVARCHAR (50) NOT NULL,
    [Phone]      NVARCHAR (50) NULL,
    [Login]      NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NOT NULL
);

