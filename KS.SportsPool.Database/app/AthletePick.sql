CREATE TABLE [app].[AthletePick]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[AthleteId] INT NOT NULL,
	[PoolEntryId] INT NOT NULL,
	[Year] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_AthletePick_To_Athlete] FOREIGN KEY ([AthleteId]) REFERENCES [app].[Athlete]([Id]),
	CONSTRAINT [FK_AthletePick_To_PoolEntry] FOREIGN KEY ([PoolEntryId]) REFERENCES [app].[PoolEntry]([Id]),
)

GO
CREATE NONCLUSTERED INDEX [IX_AthletePick_AthleteId]
    ON [app].[AthletePick]([AthleteId] ASC);
	
GO
CREATE NONCLUSTERED INDEX [IX_AthletePick_PoolEntryId]
    ON [app].[AthletePick]([PoolEntryId] ASC);