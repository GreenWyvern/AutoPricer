CREATE TABLE [dbo].[Authentication]
(
	[Username] NCHAR PRIMARY KEY,
	[Code] INTEGER,
	[ExpiryTime] DATE
)