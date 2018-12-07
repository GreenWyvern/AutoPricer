CREATE TABLE [dbo].[Authentication]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [code] INT NOT NULL, 
    [expiryTime] DATETIME NOT NULL
)
