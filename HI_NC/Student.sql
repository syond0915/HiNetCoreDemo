﻿CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StId] CHAR(4) NULL, 
    [StName] NVARCHAR(4) NULL, 
    [StTitle] NVARCHAR(10) NULL
)
