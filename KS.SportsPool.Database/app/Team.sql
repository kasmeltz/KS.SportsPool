CREATE TABLE [dbo].[Team]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] VARCHAR(50) NULL,
	[Abbreviation] VARCHAR(5) NULL,
	[Position] VARCHAR(5) NULL,
	[Conference] VARCHAR(25) NULL,
	[Division] VARCHAR(25) NULL,
	[Round1] INT NULL,
	[Round2] INT NULL,
	[Round3] INT NULL,
	[Round4] INT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
