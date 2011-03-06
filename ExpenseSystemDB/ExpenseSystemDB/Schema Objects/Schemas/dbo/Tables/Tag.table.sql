CREATE TABLE [dbo].[Tag] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [ParentId] INT           NULL,
    [UserId]   INT           NULL
);

