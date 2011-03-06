CREATE TABLE [dbo].[ExpenseRecord] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NOT NULL,
    [Price]       MONEY          NOT NULL,
    [TagId]       INT            NOT NULL,
    [DateStamp]   DATE           NOT NULL
);

