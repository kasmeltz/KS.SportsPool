CREATE TABLE [app].[TeamPick]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[TeamId] INT NOT NULL,
	[PoolEntryId] INT NOT NULL,
	[Round] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TeamPick_To_Team] FOREIGN KEY ([TeamId]) REFERENCES [app].[Team]([Id]),
	CONSTRAINT [FK_TeamPick_To_PoolEntry] FOREIGN KEY ([PoolEntryId]) REFERENCES [app].[PoolEntry]([Id]),
)

GO
CREATE NONCLUSTERED INDEX [IX_TeamPick_TeamId]
    ON [app].[TeamPick]([TeamId] ASC);
	
GO
CREATE NONCLUSTERED INDEX [IX_TeamPick_PoolEntryId]
    ON [app].[TeamPick]([PoolEntryId] ASC);