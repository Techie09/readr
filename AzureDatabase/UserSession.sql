CREATE TABLE [dbo].[UserSession]
(
	[UserSessionId] INT NOT NULL PRIMARY KEY, 
    [AppUserId] INT NOT NULL, 
    [ISBN] NVARCHAR(20) NULL, 
    [Description] NVARCHAR(200) NULL, 
    CONSTRAINT [FK_UserSession_AppUser] FOREIGN KEY (AppUserId) REFERENCES AppUser(AppUserId)
)
