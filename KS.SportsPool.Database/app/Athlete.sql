CREATE TABLE [app].[Athlete]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[FirstName] VARCHAR(30) NULL,
	[LastName] VARCHAR(30) NULL,
	[Position] VARCHAR(5) NULL,
	[Goals] INT NULL,
	[Assists] INT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
