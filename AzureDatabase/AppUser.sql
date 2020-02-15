CREATE TABLE [dbo].[AppUser]
(
	[AppUserId] INT NOT NULL Identity(1,1) PRIMARY KEY, 
    [Username] NVARCHAR(100) NOT NULL
)
