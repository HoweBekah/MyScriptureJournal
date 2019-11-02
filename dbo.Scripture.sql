CREATE TABLE [dbo].[Scripture] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [Book]    NVARCHAR (MAX) NULL,
    [Date]    DATETIME2 (7)  NOT NULL,
    [Chapter] INT            NOT NULL,
    [Verse]   NVARCHAR (MAX) NOT NULL,
    [Notes]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Scripture] PRIMARY KEY CLUSTERED ([ID] ASC)
);

