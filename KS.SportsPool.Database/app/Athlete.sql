CREATE TABLE [app].[Athlete]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[TeamId] INT NOT NULL,
	[Year] INT NOT NULL,
	[GroupName] VARCHAR(30) NULL,
	[FirstName] VARCHAR(30) NULL,
	[LastName] VARCHAR(30) NULL,
	[Position] VARCHAR(5) NULL,
	[Goals] INT NULL,
	[Assists] INT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Athlete_To_Team] FOREIGN KEY ([TeamId]) REFERENCES [app].[Team]([Id])
)

GO
CREATE NONCLUSTERED INDEX [IX_Athlete_TeamId]
    ON [app].[Athlete]([TeamId] ASC);
