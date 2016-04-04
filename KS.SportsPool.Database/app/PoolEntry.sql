﻿CREATE TABLE [app].[PoolEntry]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] VARCHAR(60) NULL,
	[Telephone] VARCHAR(15) NULL,
	[Email] VARCHAR(60) NULL,
	[Score] INT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)